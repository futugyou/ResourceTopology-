namespace KubeAgent.Monitor;

public class PersistentVolumeMonitor(ILogger<PersistentVolumeMonitor> logger, IKubernetes client, ProcessorFactory factory) : BaseMonitor(logger, factory.GetResourceProcessor()), IResourceMonitor
{
    public async Task MonitorResource(CancellationToken cancellation)
    {
        var resources = await client.CoreV1.ListPersistentVolumeWithHttpMessagesAsync(watch: true, cancellationToken: cancellation);
        resources.Watch<V1PersistentVolume, V1PersistentVolumeList>(
            onEvent: async (type, item) => await HandlerResourceChange(type, item, cancellation),
            onError: async (ex) => await HandlerError(MonitorResource, "ListPersistentVolumeWithHttpMessagesAsync", ex, cancellation));
    }
}