using Dedsi.CleanArchitecture.Domain;
using Volo.Abp.Modularity;

namespace DedsiPermission;

[DependsOn(
    typeof(DedsiCleanArchitectureDomainModule)    
)]
public class DedsiPermissionDomainModule : AbpModule;