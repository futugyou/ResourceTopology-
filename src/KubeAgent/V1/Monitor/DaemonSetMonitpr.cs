
using KubeAgent.V1.Processor;

namespace KubeAgent.V1.Monitor;

public class DaemonSetMonitpr(ILogger<DaemonSetMonitpr> logger, IKubernetes client, [FromKeyedServices("General")] IResourceProcessor processor) : BaseMonitor(logger, processor), IResourceMonitor
{
    public async Task MonitorResource(CancellationToken cancellation)
    {
        var resources = await client.AppsV1.ListDaemonSetForAllNamespacesWithHttpMessagesAsync(watch: true, cancellationToken: cancellation);
        resources.Watch<V1DaemonSet, V1DaemonSetList>(
            onEvent: async (type, item) => await HandlerResourceChange(type, item, cancellation),
            onError: async (ex) => await HandlerError(MonitorResource, "ListDaemonSetForAllNamespacesWithHttpMessagesAsync", ex, cancellation));
    }
}