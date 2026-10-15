using FirApi.Events;
using Azure.Messaging.ServiceBus;

namespace FirApi.Messaging;

public sealed class ServiceBusEventPublisher : IEventPublisher
{
    private readonly ServiceBusSender _sender;
    private readonly ILogger<ServiceBusEventPublisher> _logger;

    public ServiceBusEventPublisher(
        ServiceBusSender sender,
        ILogger<ServiceBusEventPublisher> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public async Task PublishAsync<T>(
        T integrationEvent,
        CancellationToken cancellationToken = default)
    {
        if (integrationEvent is not FIRRegisteredEvent firEvent)
        {
            throw new NotSupportedException(
                $"Unsupported event type: {typeof(T).Name}");
        }

        var message = new ServiceBusMessage(
            BinaryData.FromObjectAsJson(firEvent))
        {
            MessageId = firEvent.EventId.ToString(),
            Subject = firEvent.EventType,
            ContentType = "application/json",
            ApplicationProperties =
            {
                ["eventVersion"] = firEvent.EventVersion
            }
        };

        await _sender.SendMessageAsync(message, cancellationToken);

        _logger.LogInformation(
            "Published {EventType}, EventId: {EventId}, FirId: {FirId}",
            firEvent.EventType,
            firEvent.EventId,
            firEvent.FirId);
    }
}