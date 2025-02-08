using Dedsi.AspNetCore;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    typeof(ProjectNameCQRSUseCaseModule),
    typeof(DedsiAspNetCoreModule)
)]
public class ProjectNameCQRSMinimalApiModule : AbpModule;