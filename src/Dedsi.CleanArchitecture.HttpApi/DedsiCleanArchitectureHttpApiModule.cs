using Dedsi.AspNetCore;
using Volo.Abp.Modularity;

namespace Dedsi.CleanArchitecture.HttpApi;

[DependsOn(
    typeof(DedsiAspNetCoreModule)
)]
public class DedsiCleanArchitectureHttpApiModule : AbpModule;