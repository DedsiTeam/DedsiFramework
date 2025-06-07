using Dedsi.Ddd.Application.Contracts;
using Dedsi.Ddd.CQRS;
using Dedsi.Ddd.Domain.Shared;
using Volo.Abp.Modularity;

namespace DedsiPermission;

[DependsOn(
    typeof(DedsiDddApplicationContractsModule),
    typeof(DedsiDddDomainSharedModule),
    typeof(DedsiDddCqrsModule)
)]
public class DedsiPermissionSharedModule: AbpModule;