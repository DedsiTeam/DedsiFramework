namespace Dedsi.Cloud.Consul;

public class DedsiConsulOptions
{
    /// <summary>
    /// Consul服务器地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Http协议：http或https
    /// </summary>
    public string Protocol { get; set; }
    
    /// <summary>
    /// 服务地址
    /// </summary>
    public string ServiceAddress { get; set; }
    
    /// <summary>
    /// 服务端口
    /// </summary>
    public int ServicePort { get; set; }

    /// <summary>
    /// 健康检查URL
    /// </summary>
    public string HealthCheckUrl { get; set; }

    /// <summary>
    /// 健康检查间隔（秒）
    /// </summary>
    public int HealthCheckInterval { get; set; }

    /// <summary>
    /// 健康检查超时时间（秒）
    /// </summary>
    public int HealthCheckTimeout { get; set; }

    /// <summary>
    /// 注销临界超时时间（秒）
    /// </summary>
    public int DeregisterCriticalServiceAfter { get; set; }

    /// <summary>
    /// 服务标签
    /// </summary>
    public string[] Tags { get; set; } = [];
}
