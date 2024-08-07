using Dedsi.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dedsi.CleanArchitecture.Infrastructure;

[DependsOn(
    typeof(DedsiEntityFrameworkCoreModule)
)]
public class DedsiCleanArchitectureInfrastructureModule : AbpModule
{
    
}