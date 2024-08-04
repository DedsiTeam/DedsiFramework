using Microsoft.Extensions.DependencyInjection;

namespace Dedsi.AspNetCore;

public static class DedsiAspNetCoreExtensions
{
    public static IServiceCollection AddDedsiAspNetCore(this IServiceCollection services)
    {
        // 依赖注入
        services.AddDependencyInjection();

        return services;
    }
}