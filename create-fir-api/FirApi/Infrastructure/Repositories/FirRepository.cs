using FirApi.Application.Interfaces;
using FirApi.Domain.Entities;
using FirApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FirApi.Infrastructure.Repositories;

public class FirRepository : IFirRepository
{
    private readonly FirDbContext _context;

    public FirRepository(FirDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Fir fir,
        CancellationToken cancellationToken)
    {
        await _context.Firs.AddAsync(
            fir, cancellationToken);
    }

    public async Task<Fir?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Firs
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}