using Dedsi.SqlSugar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dedsi.AspNetCore;

public static class DedsiAspNetCoreExtensions
{
    public static IServiceCollection AddDedsiAspNetCore(
        this IServiceCollection services,
        IConfiguration configuration,
        DedsiAspNetCoreOptions options)
    {
        // 依赖注入
        services.AddDependencyInjection();
        
        // SqlSugar
        services.AddDedsiSqlSugar(configuration, options.SqlSugarOptions.ConnectionStringName, options.SqlSugarOptions.IsSqlLogging);

        return services;
    }
}