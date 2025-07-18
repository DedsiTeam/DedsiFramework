using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using Volo.Abp.Security;
using Volo.Abp.Timing;
using Volo.Abp.Validation;

namespace Dedsi.Ddd.CQRS;

[DependsOn(
    typeof(AbpGuidsModule),
    typeof(AbpEventBusAbstractionsModule),
    typeof(AbpEventBusModule),
    typeof(AbpTimingModule),
    typeof(AbpSecurityModule),
    typeof(AbpValidationModule)    
)]
public class DedsiDddCqrsModule : AbpModule;