using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Dedsi.CleanArchitecture.HttpApi;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule)
)]
public class DedsiCleanArchitectureHttpApiModule : AbpModule
{
    
}