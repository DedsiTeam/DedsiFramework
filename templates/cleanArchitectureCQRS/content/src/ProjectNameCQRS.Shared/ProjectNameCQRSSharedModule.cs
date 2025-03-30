using Dedsi.Ddd.Application.Contracts;
using Dedsi.Ddd.Domain.Shared;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    typeof(DedsiDddApplicationContractsModule),
    typeof(DedsiDddDomainSharedModule)    
)]
public class ProjectNameCQRSSharedModule: AbpModule;