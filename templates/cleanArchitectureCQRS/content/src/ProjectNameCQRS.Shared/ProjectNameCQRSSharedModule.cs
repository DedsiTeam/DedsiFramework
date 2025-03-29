using Dedsi.Ddd.Domain.Shared;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    typeof(DedsiDddDomainSharedModule)    
)]
public class ProjectNameCQRSSharedModule: AbpModule;