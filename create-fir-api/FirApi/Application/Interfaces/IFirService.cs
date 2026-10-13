using FirApi.Application.DTO;

namespace FirApi.Services;

public interface IFirService
{
    Task<FirResponseDto> CreateFirAsync(
        CreateFirRequestDto request,
        string createdBy,
        CancellationToken cancellationToken);

    Task<FirResponseDto?> GetFirByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}