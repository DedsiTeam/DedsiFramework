namespace Dedsi.SqlSugar;

public class DedsiSqlSugarOptions
{
    /// <summary>
    /// 数据库链接字符串名字
    /// </summary>
    public string ConnectionStringName { get; set; }

    /// <summary>
    /// 数据库日志
    /// </summary>
    public bool IsSqlLogging { get; set; }
}