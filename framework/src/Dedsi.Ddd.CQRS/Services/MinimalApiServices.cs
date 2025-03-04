using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.CQRS.Services;

public interface IMinimalApiServices : ITransientDependency
{
    public IDedsiMediator DedsiMediator { get; }
    
    public IHttpContextAccessor HttpContextAccessor { get; }
    
    public IConfiguration Configuration { get; }

    CancellationToken GetRequestAborted();

    ILogger<MinimalApiServices> Logger { get; }
}

public class MinimalApiServices(
    IDedsiMediator dedsiMediator,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    ILogger<MinimalApiServices> logger) : IMinimalApiServices
{
    public IDedsiMediator DedsiMediator => dedsiMediator;

    public IHttpContextAccessor HttpContextAccessor => httpContextAccessor;

    public IConfiguration Configuration => configuration;

    public CancellationToken GetRequestAborted()
    {
        if (HttpContextAccessor.HttpContext == null)
        {
            return CancellationToken.None;
        }
        return HttpContextAccessor.HttpContext.RequestAborted;
    }

    public ILogger<MinimalApiServices> Logger => logger;
}