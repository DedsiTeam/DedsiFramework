using Volo.Abp.Modularity;

namespace DedsiApiGateway;

[DependsOn(
    // DedsiApiGateway
    typeof(DedsiApiGatewayDomainModule),
    typeof(DedsiApiGatewaySharedModule),
    typeof(DedsiApiGatewayInfrastructureModule)
)]
public class DedsiApiGatewayUseCaseModule : AbpModule;