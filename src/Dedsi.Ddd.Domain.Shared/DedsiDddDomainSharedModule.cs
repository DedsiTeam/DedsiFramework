using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Domain.Shared;

[DependsOn(
    typeof(AbpDddDomainSharedModule)
)]
public class DedsiDddDomainSharedModule: AbpModule;