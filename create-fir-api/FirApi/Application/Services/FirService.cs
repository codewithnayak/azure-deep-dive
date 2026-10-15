using System.Text.Json;
using FirApi.Application.DTO;
using FirApi.Application.Interfaces;
using FirApi.Domain.Entities;
using FirApi.Events;
using FirApi.Messaging;
using IFirService = FirApi.Services.IFirService;

namespace FirApi.Application.Services;

public class FirService : IFirService
{
    private readonly IFirRepository _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<FirService> _logger;

    public FirService(IFirRepository repository , 
        IEventPublisher eventPublisher, ILogger<FirService> logger)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task<FirResponseDto> CreateFirAsync(
        CreateFirRequestDto request,
        string createdBy,
        CancellationToken cancellationToken)
    {
        if (request.IncidentDateTime == default)
        {
            throw new ArgumentException(
                "Incident date and time are required.");
        }

        if (string.IsNullOrWhiteSpace(createdBy))
        {
            throw new ArgumentException(
                "Authenticated officer identity is required.");
        }

        var firId = Guid.NewGuid();

        // Demonstration identifier only.
        // Replace with a jurisdiction-approved FIR numbering service.
        var firNumber =
            $"FIR-{DateTime.UtcNow:yyyy}-{firId:N}";

        var fir = new Fir
        {
            Id = firId,
            FirNumber = firNumber,

            PoliceStationCode = request.PoliceStationCode,
            District = request.District,
            State = request.State,

            IncidentDateTime = request.IncidentDateTime,
            IncidentDescription = request.IncidentDescription,
            IncidentLocation = request.IncidentLocation,
            CrimeCategory = request.CrimeCategory,

            ComplainantJson =
                JsonSerializer.Serialize(request.Complainant),

            AccusedPersonsJson =
                JsonSerializer.Serialize(request.AccusedPersons),

            LegalSectionsJson =
                JsonSerializer.Serialize(request.LegalSections),

            AdditionalInformation =
                request.AdditionalInformation,

            Status = FirStatus.Registered,
            CreatedAtUtc = DateTime.UtcNow,
            CreatedBy = createdBy
        };

       /* await _repository.AddAsync(
            fir, cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);*/
       
       var firRegisteredEvent = new FIRRegisteredEvent
       {
           FirId = fir.Id.ToString(),
           PoliceStationId = fir.PoliceStationCode,
           DistrictId = fir.District,
           RegisteredAtUtc = fir.CreatedAtUtc
       };
       
       await _eventPublisher.PublishAsync(
           firRegisteredEvent,
           cancellationToken);

       _logger.LogInformation(
           "FIR created and event published. FIR ID: {FirId}",
           fir.Id.ToString());

        return MapResponse(fir);
    }

    public async Task<FirResponseDto?> GetFirByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var fir = await _repository.GetByIdAsync(
            id, cancellationToken);

        return fir is null ? null : MapResponse(fir);
    }

    private static FirResponseDto MapResponse(Fir fir)
    {
        return new FirResponseDto
        {
            FirId = fir.Id,
            FirNumber = fir.FirNumber,
            Status = fir.Status.ToString(),
            CreatedAtUtc = fir.CreatedAtUtc
        };
    }
}