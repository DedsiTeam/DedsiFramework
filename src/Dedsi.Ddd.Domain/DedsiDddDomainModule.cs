using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.Domain;

[DependsOn(
    typeof(AbpDddDomainModule)
)]
public class DedsiDddDomainModule : AbpModule;