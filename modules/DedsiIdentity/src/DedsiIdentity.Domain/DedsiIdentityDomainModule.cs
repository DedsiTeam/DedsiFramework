using Dedsi.CleanArchitecture.Domain;
using Volo.Abp.Modularity;

namespace DedsiIdentity;

[DependsOn(
    typeof(DedsiCleanArchitectureDomainModule)    
)]
public class DedsiIdentityDomainModule : AbpModule;