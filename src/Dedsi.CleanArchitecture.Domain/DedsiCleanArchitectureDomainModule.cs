using Dedsi.Ddd.Application;
using Dedsi.Ddd.Domain;
using Volo.Abp.Modularity;

namespace Dedsi.CleanArchitecture.Domain;

[DependsOn(
    typeof(DedsiDddApplicationModule),
    typeof(DedsiDddDomainModule)
)]
public class DedsiCleanArchitectureDomainModule : AbpModule;