using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Volo.Abp.Modularity;

namespace Dedsi.SqlSugar.Extensions;

public static class DedsiSqlSugarExtensions
{
    /// <summary>
    /// 配置 SqlSugar : SqlServer
    /// </summary>
    /// <param name="context"></param>
    /// <param name="connectionStringName"></param>
    public static void ConfigureSqlSugarBySqlServer(this ServiceConfigurationContext context,string connectionStringName)
    {
        ConfigureSqlSugar(context, connectionStringName, DbType.SqlServer);
    }
    
    /// <summary>
    /// 配置 SqlSugar : MySql
    /// </summary>
    /// <param name="context"></param>
    /// <param name="connectionStringName"></param>
    public static void ConfigureSqlSugarByMySql(this ServiceConfigurationContext context,string connectionStringName)
    {
        ConfigureSqlSugar(context, connectionStringName, DbType.MySql);
    }
    
    /// <summary>
    /// 配置 SqlSugar
    /// </summary>
    /// <param name="context"></param>
    /// <param name="connectionStringName"></param>
    /// <param name="dbType"></param>
    public static void ConfigureSqlSugar(this ServiceConfigurationContext context,string connectionStringName,DbType dbType)
    {
        var configuration = context.Services.GetConfiguration();
        var connectionString = configuration.GetConnectionString(connectionStringName);

        var connectionConfig = new ConnectionConfig()
        {
            DbType = dbType,
            ConnectionString = connectionString,
            IsAutoCloseConnection = true,
        };
        context.ConfigureSqlSugar(connectionConfig);
    }

    /// <summary>
    /// 配置 SqlSugar
    /// </summary>
    /// <param name="context"></param>
    /// <param name="connectionConfig"></param>
    public static void ConfigureSqlSugar(this ServiceConfigurationContext context, ConnectionConfig connectionConfig)
    {
        var environment = context.Services.GetHostingEnvironment();

        context.Services.AddScoped<ISqlSugarClient>(s =>
        {
            var sqlSugarClient = new SqlSugarClient(connectionConfig);

            // 本地研发环境才会开启
            if (environment.EnvironmentName == "Development")
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