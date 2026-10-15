namespace FirApi.Events;

public sealed record FIRRegisteredEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public string EventType { get; init; } = "FIRRegistered";
    public int EventVersion { get; init; } = 1;
    public DateTime OccurredAtUtc { get; init; } = DateTime.UtcNow;

    public required string FirId { get; init; }
    public required string PoliceStationId { get; init; }
    public required string DistrictId { get; init; }
    public required DateTime RegisteredAtUtc { get; init; }
}