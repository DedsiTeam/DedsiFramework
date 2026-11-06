using System.Security.Claims;
using Dedsi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace Dedsi.Framework.Tests.Core;

public class CurrentUserTests
{
    [Fact]
    public void Claim_helpers_read_requested_claims()
    {
        var user = new Mock<ICurrentUser>();
        user.Setup(x => x.FindClaim(AbpClaimTypes.UserId)).Returns(new Claim(AbpClaimTypes.UserId, "user-1"));
        user.Setup(x => x.FindClaim(AbpClaimTypes.UserName)).Returns(new Claim(AbpClaimTypes.UserName, "alice"));
        user.Setup(x => x.FindClaim("department")).Returns(new Claim("department", "engineering"));
        Assert.Equal("user-1", user.Object.GetClaimUserId());
        Assert.Equal("alice", user.Object.GetClaimUserName());
        Assert.Equal("engineering", user.Object.GetClaimValue("department"));
        Assert.Throws<UserFriendlyException>(() => user.Object.GetClaimValue("missing"));
    }

    [Fact]
    public void Vital_log_and_user_log_include_message()
    {
        var logger = new CaptureLogger();
        logger.LogVitalInfo("important");
        Assert.Contains(logger.Messages, x => x.Contains("important"));
        var user = new Mock<ICurrentUser>();
        user.SetupGet(x => x.Name).Returns("Alice");
        user.Object.LogUserInfo(logger, "operation");
        Assert.Contains(logger.Messages, x => x.Contains("Alice") && x.Contains("operation"));
    }

    private sealed class CaptureLogger : ILogger
    {
        public List<string> Messages { get; } = [];
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => true;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) => Messages.Add(formatter(state, exception));
    }
}
