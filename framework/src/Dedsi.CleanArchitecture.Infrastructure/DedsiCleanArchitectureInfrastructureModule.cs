using Dedsi.CleanArchitecture.Domain;
using Dedsi.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dedsi.CleanArchitecture.Infrastructure;

[DependsOn(
    typeof(DedsiCleanArchitectureDomainModule),
    typeof(DedsiEntityFrameworkCoreModule)
)]
public class DedsiCleanArchitectureInfrastructureModule : AbpModule
{
    
}