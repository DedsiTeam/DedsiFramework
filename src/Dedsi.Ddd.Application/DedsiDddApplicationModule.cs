using Dedsi.Ddd.Application.Contracts;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Application;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(DedsiDddApplicationContractsModule)
)]
public class DedsiDddApplicationModule : AbpModule
{
    
}