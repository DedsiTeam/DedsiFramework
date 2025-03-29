using Dedsi.Ddd.CQRS;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    // ProjectNameCQRS
    typeof(ProjectNameCQRSDomainModule),
    typeof(ProjectNameCQRSSharedModule),
    typeof(ProjectNameCQRSInfrastructureModule),
    
    typeof(DedsiDddCqrsModule)
)]
public class ProjectNameCQRSUseCaseModule : AbpModule;