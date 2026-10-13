namespace FirApi.Domain.Entities;

public class Fir
{
    public Guid Id { get; set; }

    public string FirNumber { get; set; } = string.Empty;

    public string PoliceStationCode { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public DateTime IncidentDateTime { get; set; }

    public string IncidentDescription { get; set; } = string.Empty;

    public string IncidentLocation { get; set; } = string.Empty;

    public string? CrimeCategory { get; set; }

    public string ComplainantJson { get; set; } = "{}";

    public string AccusedPersonsJson { get; set; } = "[]";

    public string LegalSectionsJson { get; set; } = "[]";

    public string? AdditionalInformation { get; set; }

    public FirStatus Status { get; set; } = FirStatus.Registered;

    public DateTime CreatedAtUtc { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
}

public enum FirStatus
{
    Registered,
    UnderInvestigation,
    Transferred,
    Closed
}