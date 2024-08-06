using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace ProjectName.HttpApi.Host;

[DependsOn(
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule)
)]
public class ProjectNameHttpApiHostModule : AbpModule
{
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
                if (hostEnvironment.EnvironmentName == "Localhost")
                {
                    // dbConfigContext.DbContextOptions.LogTo(Serilog.Log.Information, new[] { DbLoggerCategory.Database.Command.Name }).EnableSensitiveDataLogging();
                }
                dbConfigContext.UseSqlServer();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}