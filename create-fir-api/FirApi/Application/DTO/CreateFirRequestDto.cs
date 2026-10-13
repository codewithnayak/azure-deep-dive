using System.ComponentModel.DataAnnotations;

namespace FirApi.Application.DTO;

public class CreateFirRequestDto
{
    [Required]
    public string PoliceStationCode { get; set; } = string.Empty;

    [Required]
    public string District { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public DateTime IncidentDateTime { get; set; }

    [Required]
    [StringLength(2000)]
    public string IncidentDescription { get; set; } = string.Empty;

    [Required]
    public string IncidentLocation { get; set; } = string.Empty;

    public string? CrimeCategory { get; set; }

    [Required]
    public ComplainantDto Complainant { get; set; } = new();

    public List<AccusedDto> AccusedPersons { get; set; } = new();

    public List<LegalSectionDto> LegalSections { get; set; } = new();

    public string? AdditionalInformation { get; set; }
}

public class ComplainantDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    public int? Age { get; set; }

    [Phone]
    public string? ContactNumber { get; set; }

    public string? Address { get; set; }
}

public class AccusedDto
{
    public string? FullName { get; set; }

    public string? Description { get; set; }

    public bool IsIdentityKnown { get; set; }
}

public class LegalSectionDto
{
    [Required]
    public string ActCode { get; set; } = string.Empty;

    [Required]
    public string SectionNumber { get; set; } = string.Empty;

    public string? Description { get; set; }
}