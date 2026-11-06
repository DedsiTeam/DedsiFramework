using Dedsi.Core;
using Dedsi.Ddd.Domain.Shared;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Domain;

[DependsOn(
    typeof(DedsiCoreModule),
    typeof(DedsiDddDomainSharedModule),
    typeof(AbpDddDomainModule)
)]
public class DedsiDddDomainModule : AbpModule;