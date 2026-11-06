using Dedsi.Core;
using Dedsi.Ddd.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Application.Contracts;

[DependsOn(
    typeof(DedsiCoreModule),
    typeof(DedsiDddDomainSharedModule),
    typeof(AbpFluentValidationModule),
    typeof(AbpDddApplicationContractsModule)
)]
public class DedsiDddApplicationContractsModule : AbpModule;