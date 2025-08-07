using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Consul;

namespace Dedsi.Cloud.Consul;

public static class DedsiConsulExtensions
{
    public static void UseConsul(this IApplicationBuilder app)
    {
        var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        var logger = app.ApplicationServices.GetRequiredService<ILogger<ConsulClient>>();

        // 读取Consul配置
        var consulOptions = new DedsiConsulOptions();
        configuration.GetSection("Consul").Bind(consulOptions);

        // 验证必要配置
        if (string.IsNullOrEmpty(consulOptions.ServiceName))
        {
            logger.LogWarning("Consul服务名称未配置，跳过Consul注册");
            return;
        }

        // 创建Consul客户端
        var consulClient = new ConsulClient(config =>
        {
            config.Address = new Uri(consulOptions.Address);
        });

        // 构建健康检查URL
        var healthCheckUrl = $"{consulOptions.Protocol}://{consulOptions.ServiceAddress}:{consulOptions.ServicePort}{consulOptions.HealthCheckUrl}";

        // 注册服务
        var registration = new AgentServiceRegistration
        {
            ID = consulOptions.ServiceName + $"-{Guid.CreateVersion7().ToString().Replace("-","")}",
            Name = consulOptions.ServiceName,
            Address = consulOptions.ServiceAddress,
            Port = consulOptions.ServicePort,
            Tags = consulOptions.Tags,
            Check = new AgentServiceCheck
            {
                HTTP = healthCheckUrl,
                Interval = TimeSpan.FromSeconds(consulOptions.HealthCheckInterval),
                Timeout = TimeSpan.FromSeconds(consulOptions.HealthCheckTimeout),
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulOptions.DeregisterCriticalServiceAfter)
            }
        };

        // 应用启动时注册服务
        lifetime.ApplicationStarted.Register(async void () =>
        {
            try
            {
                await consulClient.Agent.ServiceRegister(registration);
                logger.LogInformation("服务 {ServiceName} (ID: {ServiceId}) 已成功注册到Consul", registration.Name, registration.ID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "注册服务到Consul失败");
            }
        });

        // 应用停止时注销服务
        lifetime.ApplicationStopping.Register(async void () =>
        {
            try
            {
                await consulClient.Agent.ServiceDeregister(registration.ID);
                logger.LogInformation("服务 {ServiceName} (ID: {ServiceId}) 已从Consul注销", registration.Name, registration.ID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "从Consul注销服务失败");
            }
            finally
            {
                consulClient.Dispose();
            }
        });
    }
}