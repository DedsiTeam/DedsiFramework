using Dedsi.Ddd.Domain;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Dedsi.SqlSugar;

[DependsOn(
    typeof(DedsiDddDomainModule),
    typeof(AbpAspNetCoreMvcModule)
)]
public class DedsiSqlSugarModule : AbpModule;