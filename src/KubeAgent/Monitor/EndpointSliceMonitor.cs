namespace KubeAgent.Monitor;

public class EndpointSliceMonitor(ILogger<EndpointSliceMonitor> logger, IKubernetes client, ProcessorFactory factory) : BaseMonitor(logger, factory.GetResourceProcessor()), IResourceMonitor
{
    public async Task MonitorResource(CancellationToken cancellation)
    {
        var resources = await client.DiscoveryV1.ListEndpointSliceForAllNamespacesWithHttpMessagesAsync(watch: true, cancellationToken: cancellation);
        resources.Watch<V1EndpointSlice, V1EndpointSliceList>(
            onEvent: async (type, item) => await HandlerResourceChange(type, item, cancellation),
            onError: async (ex) => await HandlerError(MonitorResource, "ListEndpointSliceForAllNamespacesWithHttpMessagesAsync", ex, cancellation));
    }
}