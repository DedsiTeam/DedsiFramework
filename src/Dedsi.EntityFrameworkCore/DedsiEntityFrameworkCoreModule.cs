using Dedsi.Ddd.Domain;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dedsi.EntityFrameworkCore;

[DependsOn(
    typeof(DedsiDddDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DedsiEntityFrameworkCoreModule : AbpModule;