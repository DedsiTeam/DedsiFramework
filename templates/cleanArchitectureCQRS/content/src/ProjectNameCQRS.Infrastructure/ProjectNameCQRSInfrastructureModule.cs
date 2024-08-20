using Dedsi.CleanArchitecture.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ProjectNameCQRS.Emails;
using ProjectNameCQRS.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    typeof(DedsiCleanArchitectureInfrastructureModule)
)]
public class ProjectNameCQRSInfrastructureModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // EntityFrameworkCore
        context.Services.AddAbpDbContext<ProjectNameCQRSDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });

        // 测试发送邮件
        context.Services.AddTransient<IEmailSender, TestEmailSender>();
    }
}