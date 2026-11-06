using System.Text.Json;
using Volo.Abp.Http.Client;
using Volo.Abp.Http;
using Dedsi.Ddd.Application.Contracts.Dtos;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Dedsi.AspNetCore;
using Dedsi.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp;
using Volo.Abp.Validation;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Dedsi.Ddd.Application.Contracts;

namespace Dedsi.Framework.Tests.Http;

public class HttpTests
{
    private sealed class Controller : DedsiControllerBase
    {
        public FileContentResult Excel(byte[] bytes) => FileByExcel(bytes, "报表.xlsx");
        public FileStreamResult Excel(Stream stream) => FileByExcel(stream, "报表.xlsx");
        public FileContentResult Pdf(byte[] bytes) => FileByPdf(bytes, "报表.pdf");
    }

    [Fact]
    public void Download_helpers_preserve_payload_name_and_media_type()
    {
        var controller = new Controller();
        byte[] bytes = [1, 2, 3];
        var excel = controller.Excel(bytes);
        Assert.Same(bytes, excel.FileContents);
        Assert.Equal("报表.xlsx", excel.FileDownloadName);
        Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excel.ContentType);
        using var stream = new MemoryStream(bytes);
        var streamed = controller.Excel(stream);
        Assert.Same(stream, streamed.FileStream);
        Assert.Equal(excel.ContentType, streamed.ContentType);
        Assert.Equal(excel.FileDownloadName, streamed.FileDownloadName);
        var pdf = controller.Pdf(bytes);
        Assert.Same(bytes, pdf.FileContents);
        Assert.Equal("application/pdf", pdf.ContentType);
        Assert.Equal("报表.pdf", pdf.FileDownloadName);
    }

    private static async Task<(int Status, JsonElement Error)> Handle(
        Exception exception, bool authenticated = false, Action<AbpExceptionHttpStatusCodeOptions>? configureStatus = null, Action<DedsiExceptionHandlingOptions>? configureRemote = null)
    {
        var registrations = new ServiceCollection();
        using var app = registrations.AddApplication<DedsiAspNetCoreModule>();
        // Even when the host opts into detailed errors, this public handler must remain safe.
        registrations.Configure<AbpExceptionHandlingOptions>(options =>
        {
            options.SendExceptionsDetailsToClients = true;
            options.SendStackTraceToClients = true;
        });
        if (configureStatus != null) registrations.Configure(configureStatus);
        if (configureRemote != null) registrations.Configure(configureRemote);
        registrations.AddExceptionHandler<DedsiGlobalExceptionHandler>();
        using var services = registrations.BuildServiceProvider();
        using var scope = services.CreateScope();
        var context = new DefaultHttpContext { RequestServices = scope.ServiceProvider };
        context.User = new ClaimsPrincipal(new ClaimsIdentity(authenticated ? "test" : null));
        await using var stream = new MemoryStream();
        context.Response.Body = stream;
        var logger = new Mock<ILogger<DedsiGlobalExceptionHandler>>();
        var handler = new DedsiGlobalExceptionHandler(logger.Object);
        Assert.True(await handler.TryHandleAsync(context, exception, default));
        Assert.StartsWith("application/json", context.Response.ContentType);
        Assert.Contains(logger.Invocations, call => call.Method.Name == "Log" && ReferenceEquals(call.Arguments[3], exception));
        stream.Position = 0;
        using var doc = await JsonDocument.ParseAsync(stream);
        return (context.Response.StatusCode, doc.RootElement.GetProperty("error").Clone());
    }

    [Theory]
    [InlineData("ORDER:001")]
    [InlineData(null)]
    [InlineData(" ")]
    public async Task Friendly_error_preserves_message_and_uses_code_fallback(string? code)
    {
        var (status, error) = await Handle(new UserFriendlyException("库存不足", code));
        Assert.Equal(403, status);
        Assert.Equal("库存不足", error.GetProperty("message").GetString());
        Assert.Equal(string.IsNullOrWhiteSpace(code) ? "global-error-403" : code, error.GetProperty("code").GetString());
    }

    [Fact]
    public async Task Business_error_preserves_business_code()
    {
        var (_, error) = await Handle(new BusinessException("ORDER:002"));
        Assert.Equal("ORDER:002", error.GetProperty("code").GetString());
    }

    [Fact]
    public async Task Internal_exception_should_hide_sensitive_details()
    {
        var exception = new InvalidOperationException("database-password=secret", new IOException("secret path"));
        exception.Data["password"] = "secret";
        var (status, error) = await Handle(exception);
        Assert.Equal(500, status);
        Assert.Equal("global-error-500", error.GetProperty("code").GetString());
        Assert.False(string.IsNullOrWhiteSpace(error.GetProperty("message").GetString()));
        Assert.DoesNotContain("secret", error.GetRawText());
        Assert.Equal(new[] { "code", "message" }, error.EnumerateObject().Select(x => x.Name).OrderBy(x => x));
    }

    [Fact]
    public async Task Validation_error_should_return_bad_request()
    {
        var (status, _) = await Handle(new AbpValidationException("invalid request"));
        Assert.Equal(400, status);
    }
    [Theory]
    [InlineData(false, 401)]
    [InlineData(true, 403)]
    public async Task Authorization_uses_authentication_state(bool authenticated, int expected)
    {
        var (status, error) = await Handle(new AbpAuthorizationException("internal permission detail"), authenticated);
        Assert.Equal(expected, status);
        Assert.Equal($"global-error-{expected}", error.GetProperty("code").GetString());
        Assert.DoesNotContain("internal permission detail", error.GetRawText());
    }

    [Fact]
    public async Task Missing_entity_and_concurrency_use_abp_status_codes()
    {
        var (missingStatus, _) = await Handle(new EntityNotFoundException(typeof(Controller), Guid.NewGuid()));
        Assert.Equal(404, missingStatus);
        var (conflictStatus, _) = await Handle(new AbpDbConcurrencyException("internal conflict detail"));
        Assert.Equal(409, conflictStatus);
    }

    [Fact]
    public async Task Business_status_mapping_is_configurable_and_internal_message_is_hidden()
    {
        var (status, error) = await Handle(new BusinessException("ORDER:Conflict", "secret sql"),
            configureStatus: options => options.Map("ORDER:Conflict", HttpStatusCode.Conflict));
        Assert.Equal(409, status);
        Assert.Equal("ORDER:Conflict", error.GetProperty("code").GetString());
        Assert.DoesNotContain("secret", error.GetRawText());
    }

    [Fact]
    public async Task Friendly_error_does_not_expose_details_or_data()
    {
        var exception = new UserFriendlyException("库存不足", "ORDER:001", "secret detail");
        exception.Data["private"] = "secret";
        var (_, error) = await Handle(exception);
        Assert.Equal("库存不足", error.GetProperty("message").GetString());
        Assert.DoesNotContain("secret", error.GetRawText());
    }

    [Fact]
    public async Task Started_response_is_left_to_host_without_writing()
    {
        var response = new Mock<IHttpResponseFeature>();
        response.SetupGet(x => x.HasStarted).Returns(true);
        var context = new DefaultHttpContext();
        context.Features.Set(response.Object);
        var handler = new DedsiGlobalExceptionHandler(NullLogger<DedsiGlobalExceptionHandler>.Instance);
        Assert.False(await handler.TryHandleAsync(context, new Exception(), default));
        response.VerifySet(x => x.StatusCode = It.IsAny<int>(), Times.Never);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Aborted_request_is_left_to_host_without_error_body(bool wrapped)
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        var context = new DefaultHttpContext { RequestAborted = cts.Token };
        await using var stream = new MemoryStream();
        context.Response.Body = stream;
        var handler = new DedsiGlobalExceptionHandler(NullLogger<DedsiGlobalExceptionHandler>.Instance);
        Exception exception = new OperationCanceledException(cts.Token);
        Assert.False(await handler.TryHandleAsync(context, wrapped ? new AggregateException(exception) : exception, cts.Token));
        Assert.Equal(0, stream.Length);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Remote_error_never_trusts_upstream_message(bool withErrorObject)
    {
        Exception remote = withErrorObject
            ? new AbpRemoteCallException(new RemoteServiceErrorInfo("secret connection", "secret stack", "secret code"))
            : new AbpRemoteCallException("secret connection");
        var (status, error) = await Handle(remote);
        Assert.Equal(500, status); // ABP 默认规则，不强制改成 502。
        Assert.DoesNotContain("secret", error.GetRawText());
        Assert.Equal("global-error-500", error.GetProperty("code").GetString());
    }

    [Fact]
    public async Task Remote_business_error_uses_explicit_local_message()
    {
        var remote = new AbpRemoteCallException(new RemoteServiceErrorInfo("secret", "secret", "ORDER:Unavailable"));
        var (_, error) = await Handle(remote, configureRemote: options => options.RemoteErrorMessages.Add("ORDER:Unavailable", "库存不足"));
        Assert.Equal("库存不足", error.GetProperty("message").GetString());
        Assert.Equal("ORDER:Unavailable", error.GetProperty("code").GetString());
        Assert.DoesNotContain("secret", error.GetRawText());
    }

    [Fact]
    public async Task Single_wrapped_validation_uses_consistent_status_and_message()
    {
        var (status, error) = await Handle(new AggregateException(new AggregateException(new AbpValidationException("secret"))));
        Assert.Equal(400, status);
        Assert.Equal("global-error-400", error.GetProperty("code").GetString());
        Assert.DoesNotContain("secret", error.GetRawText());
    }

    [Fact]
    public async Task Multiple_wrapped_errors_do_not_pick_first_business_message()
    {
        var (status, error) = await Handle(new AggregateException(new UserFriendlyException("first secret"), new Exception("second secret")));
        Assert.Equal(500, status);
        Assert.DoesNotContain("secret", error.GetRawText());
    }

    [Fact]
    public async Task Wrapped_remote_error_remains_sanitized()
    {
        var (_, error) = await Handle(new AggregateException(new AbpRemoteCallException("secret")));
        Assert.DoesNotContain("secret", error.GetRawText());
    }

    [Theory]
    [InlineData(false, 401)]
    [InlineData(true, 403)]
    public async Task Wrapped_authorization_preserves_abp_status(bool authenticated, int expected)
    {
        var (status, _) = await Handle(new AggregateException(new AbpAuthorizationException()), authenticated);
        Assert.Equal(expected, status);
    }

    [Fact]
    public async Task Wrapped_business_error_preserves_configured_status()
    {
        var (status, error) = await Handle(new AggregateException(new BusinessException("ORDER:Conflict", "secret")),
            configureStatus: options => options.Map("ORDER:Conflict", HttpStatusCode.Conflict));
        Assert.Equal(409, status);
        Assert.Equal("ORDER:Conflict", error.GetProperty("code").GetString());
        Assert.DoesNotContain("secret", error.GetRawText());
    }

    [Fact]
    public async Task Paging_overflow_is_rejected_by_abp_validation_and_returns_400()
    {
        using var app = await AbpApplicationFactory.CreateAsync<PagingValidationTestModule>(options => options.UseAutofac());
        await app.InitializeAsync();
        var validator = app.ServiceProvider.GetRequiredService<IObjectValidator>();
        await validator.ValidateAsync(new DedsiPagedRequestDto { PageIndex = 2, PageSize = 10 });
        await validator.ValidateAsync(new DedsiPagedRequestDto { PageIndex = int.MaxValue, PageSize = 1 });
        var exception = await Assert.ThrowsAsync<AbpValidationException>(() =>
            validator.ValidateAsync(new DedsiPagedRequestDto { PageIndex = int.MaxValue, PageSize = 10 }));
        Assert.Contains(exception.ValidationErrors, error => error.MemberNames.Contains("PageIndex"));
        var (status, _) = await Handle(exception);
        Assert.Equal(400, status);
        // 内部编程错误仍使用 ABP 500，而不是将所有 ArgumentException 映射成 400。
        Assert.Equal(500, (await Handle(new ArgumentOutOfRangeException("internal"))).Status);
    }

}

[DependsOn(typeof(AbpAutofacModule), typeof(DedsiDddApplicationContractsModule))]
public class PagingValidationTestModule : AbpModule
{
    public PagingValidationTestModule() => SkipAutoServiceRegistration = true;
}
