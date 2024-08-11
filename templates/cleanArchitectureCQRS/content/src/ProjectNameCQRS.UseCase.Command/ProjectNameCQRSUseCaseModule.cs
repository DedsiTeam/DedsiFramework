using System.Reflection;
using Dedsi.Ddd.CQRS;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    // ProjectNameCQRS
    typeof(ProjectNameCQRSDomainModule),
    typeof(ProjectNameCQRSInfrastructureModule),
    
    typeof(DedsiDddCQRSModule)
)]
public class ProjectNameCQRSUseCaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // MediatR
        context.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}