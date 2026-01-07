using Volo.Abp.Modularity;
using Volo.Abp.Security;

namespace Dedsi.Core;

[DependsOn(
    typeof(AbpSecurityModule)
)]
public class DedsiCoreModule : AbpModule;