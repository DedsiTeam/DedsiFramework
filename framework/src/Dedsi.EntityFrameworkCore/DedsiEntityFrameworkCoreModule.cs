using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dedsi.EntityFrameworkCore;

[DependsOn(
    typeof(AbpDapperModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DedsiEntityFrameworkCoreModule : AbpModule
{
}