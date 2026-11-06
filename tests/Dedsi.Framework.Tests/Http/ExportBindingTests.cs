using System.Globalization;
using Dedsi.Ddd.Application.Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Dedsi.Framework.Tests.Http;

public class ExportBindingTests
{
    [Theory]
    [InlineData(false, "IsExport")]
    [InlineData(false, "isExport")]
    [InlineData(true, "IsExport")]
    [InlineData(true, "isExport")]
    public async Task Client_binding_cannot_enable_export(bool form, string fieldName)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddControllers();
        using var provider = services.BuildServiceProvider();
        var metadata = provider.GetRequiredService<IModelMetadataProvider>().GetMetadataForType(typeof(DedsiPagedRequestDto));
        var binder = provider.GetRequiredService<IModelBinderFactory>().CreateBinder(new ModelBinderFactoryContext { Metadata = metadata, BindingInfo = new BindingInfo() });
        var context = new DefaultHttpContext { RequestServices = provider };
        var values = new Dictionary<string, StringValues> { [fieldName] = "true", ["PageIndex"] = "2" };
        IValueProvider valueProvider = form
            ? new FormValueProvider(BindingSource.Form, new FormCollection(values), CultureInfo.InvariantCulture)
            : new QueryStringValueProvider(BindingSource.Query, new QueryCollection(values), CultureInfo.InvariantCulture);
        var binding = DefaultModelBindingContext.CreateBindingContext(
            new ActionContext(context, new RouteData(), new ActionDescriptor()),
            valueProvider, metadata, new BindingInfo(), "");
        await binder.BindModelAsync(binding);
        var request = Assert.IsType<DedsiPagedRequestDto>(binding.Result.Model);
        Assert.Equal(2, request.PageIndex);
        Assert.False(request.IsExport);
        request.EnableExport();
        Assert.True(request.IsExport);
    }
}
