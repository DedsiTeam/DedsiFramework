using Dedsi.SqlSugar;

namespace Dedsi.AspNetCore;

public class DedsiAspNetCoreOptions
{
    public DedsiAspNetCoreOptions()
    {
        this.SqlSugarOptions = new DedsiSqlSugarOptions()
        {
            ConnectionStringName = "Default",
            IsSqlLogging = true
        };
    }
    
    public DedsiSqlSugarOptions SqlSugarOptions { get; set; }
}