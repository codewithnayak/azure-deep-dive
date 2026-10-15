namespace FirApi.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<T>(
        T integrationEvent,
        CancellationToken cancellationToken = default);
}