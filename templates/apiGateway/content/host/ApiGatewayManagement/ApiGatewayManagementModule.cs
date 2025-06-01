using System.Reflection;
using System.Text;
using ApiGateway;
using Dedsi.AspNetCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace ApiGatewayManagement;

[DependsOn(
    typeof(ApiGatewayUseCaseModule),

    typeof(DedsiAspNetCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpAutofacModule)
)]
public class ApiGatewayManagementModule : AbpModule
{
    private readonly string[] _useCaseModuleNames =
    [
        "ApiGateway.UseCase"
    ];

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostEnvironment = context.Services.GetAbpHostEnvironment();

        // 数据库
        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(dbConfigContext =>
            {
                // 本地研发环境 - 输出到控制台
                if (hostEnvironment.EnvironmentName == "Development")
                {
                    dbConfigContext.DbContextOptions.LogTo(Serilog.Log.Information, new[] { DbLoggerCategory.Database.Command.Name }).EnableSensitiveDataLogging();
                }
                dbConfigContext.UseSqlServer();
            });
        });
        
        // 日志
        Configure<AbpAuditingOptions>(options =>
        {
            options.ApplicationName = ApiGatewayDomainConsts.ApplicationName;
            options.IsEnabledForGetRequests = true;
        });
        
        // 时间格式 
        Configure<AbpJsonOptions>(options =>
        {
            options.OutputDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        });

        // 跨域
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? []
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // MediatR
        context.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(_useCaseModuleNames.Select(Assembly.Load).ToArray()));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAntiforgery();
        app.UseAuthorization();
        app.UseAuditing();

        app.UseConfiguredEndpoints();
    }
}