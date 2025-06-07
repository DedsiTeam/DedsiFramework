using Volo.Abp.Modularity;

namespace DedsiPermission;

[DependsOn(
    // DedsiPermission
    typeof(DedsiPermissionDomainModule),
    typeof(DedsiPermissionSharedModule),
    typeof(DedsiPermissionInfrastructureModule)
)]
public class DedsiPermissionUseCaseModule : AbpModule;