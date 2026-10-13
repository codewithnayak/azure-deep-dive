namespace FirApi.Application.DTO;

public class FirResponseDto
{
    public Guid FirId { get; set; }

    public string FirNumber { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}