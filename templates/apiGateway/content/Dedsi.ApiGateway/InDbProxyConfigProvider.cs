using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Dedsi.ApiGateway;

public class InDbProxyConfigProvider : IProxyConfigProvider
{
    private readonly Timer _timer;

    private InDBConfig _config;

    public InDbProxyConfigProvider()
    {
        _config = CreateConfig();

        // 每60秒刷新一次
        _timer = new Timer(_ => RefreshConfig(), null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
    }

    private void RefreshConfig()
    {
        var newConfig = CreateConfig();
        var oldConfig = Interlocked.Exchange(ref _config, newConfig);
        oldConfig.SignalChange();
    }

    public void Dispose() => _timer.Dispose();

    public IProxyConfig GetConfig() => _config;

    private InDBConfig CreateConfig()
    {
        return new InDBConfig(
        [
            new RouteConfig
            {
                RouteId = "forDBRoute",
                ClusterId = "forDBCluster",
                Match = new RouteMatch
                {
                    Path = "/{**catch-all}"
                }
            }
        ],
        [
            new ClusterConfig
            {
                ClusterId = "forDBCluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "forDBDestination", new DestinationConfig { Address = DateTime.Now.Microsecond % 2 == 0 ? "https://www.jd.com/" : "https://www.baidu.com/" } }
                }
            }
        ]);
    }

    private sealed class InDBConfig : IProxyConfig
    {
        // Used to implement the change token for the state
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public InDBConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
            : this(routes, clusters, Guid.NewGuid().ToString())
        { }

        public InDBConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters, string revisionId)
        {
            RevisionId = revisionId ?? throw new ArgumentNullException(nameof(revisionId));
            Routes = routes;
            Clusters = clusters;
            ChangeToken = new CancellationChangeToken(_cts.Token);
        }

        /// <inheritdoc/>
        public string RevisionId { get; }

        /// <summary>
        /// A snapshot of the list of routes for the proxy
        /// </summary>
        public IReadOnlyList<RouteConfig> Routes { get; }

        /// <summary>
        /// A snapshot of the list of Clusters which are collections of interchangeable destination endpoints
        /// </summary>
        public IReadOnlyList<ClusterConfig> Clusters { get; }

        /// <summary>
        /// Fired to indicate the proxy state has changed, and that this snapshot is now stale
        /// </summary>
        public IChangeToken ChangeToken { get; }

        internal void SignalChange()
        {
            _cts.Cancel();
        }
    }
}
