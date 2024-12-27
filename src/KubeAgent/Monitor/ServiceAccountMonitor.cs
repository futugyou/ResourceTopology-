namespace KubeAgent.Monitor;

public class ServiceAccountMonitor(ILogger<ServiceAccountMonitor> logger, IKubernetes client, ProcessorFactory factory) : BaseMonitor(logger, factory.GetResourceProcessor()), IResourceMonitor
{
    public async Task MonitorResource(CancellationToken cancellation)
    {
        var resources = await client.CoreV1.ListServiceAccountForAllNamespacesWithHttpMessagesAsync(watch: true, cancellationToken: cancellation);
        resources.Watch<V1ServiceAccount, V1ServiceAccountList>(
            onEvent: async (type, item) => await HandlerResourceChange(type, item, cancellation),
            onError: async (ex) => await HandlerError(MonitorResource, "ListServiceAccountForAllNamespacesWithHttpMessagesAsync", ex, cancellation));
    }
}