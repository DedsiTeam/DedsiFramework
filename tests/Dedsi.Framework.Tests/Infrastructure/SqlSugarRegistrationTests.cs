using Dedsi.SqlSugar.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using Volo.Abp.Modularity;

namespace Dedsi.Framework.Tests.Infrastructure;

public class SqlSugarRegistrationTests
{
    [Theory]
    [InlineData(true, "Development")]
    [InlineData(false, "Production")]
    public void Configuration_selects_provider_and_scopes_clients(bool sqlServer, string environmentName)
    {
        var environment = new Mock<IWebHostEnvironment>();
        environment.SetupGet(x => x.EnvironmentName).Returns(environmentName);
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> {
            ["ConnectionStrings:Test"] = "Server=localhost;Database=Test;User ID=test;Password=test"
        }).Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton(environment.Object);
        services.AddSingleton<IHostEnvironment>(environment.Object);
        var context = new ServiceConfigurationContext(services);
        if (sqlServer) context.ConfigureSqlSugarBySqlServer("Test");
        else context.ConfigureSqlSugarByMySql("Test");
        using var provider = services.BuildServiceProvider();
        using var first = provider.CreateScope();
        using var second = provider.CreateScope();
        var client = first.ServiceProvider.GetRequiredService<ISqlSugarClient>();
        Assert.Same(client, first.ServiceProvider.GetRequiredService<ISqlSugarClient>());
        Assert.NotSame(client, second.ServiceProvider.GetRequiredService<ISqlSugarClient>());
        Assert.Equal(sqlServer ? DbType.SqlServer : DbType.MySql, client.CurrentConnectionConfig.DbType);
        Assert.Equal(configuration.GetConnectionString("Test"), client.CurrentConnectionConfig.ConnectionString);
        Assert.True(client.CurrentConnectionConfig.IsAutoCloseConnection);
        // No database connection is opened by this registration test.
    }
}
