using Dedsi.Ddd.Application.Contracts;
using Dedsi.Core;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Dedsi.AspNetCore;

[DependsOn(
    typeof(DedsiDddApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(DedsiCoreModule)
)]
public class DedsiAspNetCoreModule : AbpModule;