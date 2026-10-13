using FirApi.Domain.Entities;

namespace FirApi.Application.Interfaces;

public interface IFirRepository
{
    Task AddAsync(
        Fir fir,
        CancellationToken cancellationToken);

    Task<Fir?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}