using Dedsi.CleanArchitecture.Domain;
using Volo.Abp.Modularity;

namespace DedsiApiGateway;

[DependsOn(
    typeof(DedsiCleanArchitectureDomainModule)    
)]
public class DedsiApiGatewayDomainModule : AbpModule;