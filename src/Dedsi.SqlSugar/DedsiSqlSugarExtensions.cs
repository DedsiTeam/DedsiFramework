using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Dedsi.SqlSugar;

public static class DedsiSqlSugarExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="connectionStringName"></param>
    /// <param name="isSqlLogging"></param>
    public static void AddDedsiSqlSugar(this IServiceCollection services, 
        IConfiguration configuration,
        string connectionStringName,
        bool isSqlLogging = true)
    {
        var connectionConfig = new ConnectionConfig()
        {
            DbType = DbType.SqlServer,
            ConnectionString = configuration.GetConnectionString(connectionStringName),
            IsAutoCloseConnection = true,
        };
        services.AddDedsiSqlSugar(connectionConfig, isSqlLogging);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionConfig"></param>
    /// <param name="isSqlLogging"></param>
    public static void AddDedsiSqlSugar(
        this IServiceCollection services,
        ConnectionConfig connectionConfig,
        bool isSqlLogging)
    {
        services.AddScoped<ISqlSugarClient>(s =>
        {
            var sqlSugarClient = new SqlSugarClient(connectionConfig);

            // 本地研发环境才会开启
            if (isSqlLogging)
            {
                sqlSugarClient.Aop.OnLogExecuting = (sql, parameters) =>
                {
                    Console.WriteLine(UtilMethods.GetSqlString(connectionConfig.DbType, sql, parameters));
                };
            }
            return sqlSugarClient;
        });
    }

}