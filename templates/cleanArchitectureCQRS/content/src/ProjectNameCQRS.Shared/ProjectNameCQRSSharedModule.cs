using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    typeof(AbpDddDomainSharedModule)    
)]
public class ProjectNameCQRSSharedModule: AbpModule;