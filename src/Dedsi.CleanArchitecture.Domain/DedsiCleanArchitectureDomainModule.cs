using Dedsi.Ddd.Domain;
using Volo.Abp.Modularity;

namespace Dedsi.CleanArchitecture.Domain;

[DependsOn(
    typeof(DedsiDddDomainModule)
)]
public class DedsiCleanArchitectureDomainModule : AbpModule;