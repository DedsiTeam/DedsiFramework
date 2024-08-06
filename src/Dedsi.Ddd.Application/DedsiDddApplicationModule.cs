using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Application;

[DependsOn(
    typeof(AbpDddApplicationContractsModule)
)]
public class DedsiDddApplicationModule : AbpModule
{
    
}