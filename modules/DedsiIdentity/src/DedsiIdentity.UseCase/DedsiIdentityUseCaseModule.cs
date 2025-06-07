using Volo.Abp.Modularity;

namespace DedsiIdentity;

[DependsOn(
    // DedsiIdentity
    typeof(DedsiIdentityDomainModule),
    typeof(DedsiIdentitySharedModule),
    typeof(DedsiIdentityInfrastructureModule)
)]
public class DedsiIdentityUseCaseModule : AbpModule;