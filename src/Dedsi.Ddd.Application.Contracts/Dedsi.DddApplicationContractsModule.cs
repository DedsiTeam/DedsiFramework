using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Application.Contracts;

[DependsOn(
    typeof(AbpDddApplicationContractsModule)
)]
public class Dedsi_DddApplicationContractsModule : AbpModule
{
    
}