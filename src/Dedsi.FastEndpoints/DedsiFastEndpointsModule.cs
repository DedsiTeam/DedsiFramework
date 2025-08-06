using Dedsi.AspNetCore;
using Volo.Abp.Modularity;

namespace Dedsi.FastEndpoints;

[DependsOn(
    typeof(DedsiAspNetCoreModule)    
)]
public class DedsiFastEndpointsModule: AbpModule;