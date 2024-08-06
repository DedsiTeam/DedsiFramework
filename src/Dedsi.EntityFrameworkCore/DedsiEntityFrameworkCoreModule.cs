using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dedsi.EntityFrameworkCore;

[DependsOn(
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DedsiEntityFrameworkCoreModule : AbpModule
{
}