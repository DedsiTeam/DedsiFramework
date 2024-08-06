using Dedsi.Core;
using Volo.Abp.Modularity;

namespace Dedsi.AspNetCore;

[DependsOn(
    typeof(DedsiCoreModule)
)]
public class DedsiAspNetCoreModule : AbpModule
{
    
}