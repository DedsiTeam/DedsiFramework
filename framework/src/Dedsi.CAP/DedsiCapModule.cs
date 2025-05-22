using Dedsi.AspNetCore;
using Volo.Abp.Modularity;

namespace Dedsi.CAP;

[DependsOn(
    typeof(DedsiAspNetCoreModule)    
)]
public class DedsiCapModule: AbpModule;
