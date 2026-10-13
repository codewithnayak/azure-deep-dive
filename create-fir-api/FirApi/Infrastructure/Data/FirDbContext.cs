using FirApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirApi.Infrastructure.Data;

public class FirDbContext : DbContext
{
    public FirDbContext(DbContextOptions<FirDbContext> options)
        : base(options)
    {
    }

    public DbSet<Fir> Firs => Set<Fir>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fir>(entity =>
        {
            entity.ToTable("Firs");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FirNumber)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(x => x.FirNumber)
                .IsUnique();

            entity.Property(x => x.PoliceStationCode)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.IncidentDescription)
                .HasMaxLength(2000);

            entity.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(x => x.ComplainantJson)
                .HasColumnType("nvarchar(max)");

            entity.Property(x => x.AccusedPersonsJson)
                .HasColumnType("nvarchar(max)");

            entity.Property(x => x.LegalSectionsJson)
                .HasColumnType("nvarchar(max)");
        });
    }
}