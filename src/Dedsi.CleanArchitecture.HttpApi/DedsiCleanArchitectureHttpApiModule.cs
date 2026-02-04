using Dedsi.AspNetCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Dedsi.CleanArchitecture.HttpApi;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(DedsiAspNetCoreModule)
)]
public class DedsiCleanArchitectureHttpApiModule : AbpModule;