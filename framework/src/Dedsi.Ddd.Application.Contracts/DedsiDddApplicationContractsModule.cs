using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Application.Contracts;

[DependsOn(
    typeof(AbpFluentValidationModule),
    typeof(AbpDddApplicationContractsModule)
)]
public class DedsiDddApplicationContractsModule : AbpModule
{
    
}