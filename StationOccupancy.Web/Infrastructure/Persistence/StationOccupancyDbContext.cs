using Microsoft.EntityFrameworkCore;
using StationOccupancy.Web.Domain.Stations;

namespace StationOccupancy.Web.Infrastructure.Persistence;

public sealed class StationOccupancyDbContext : DbContext
{
    public StationOccupancyDbContext(DbContextOptions<StationOccupancyDbContext> options) : base(options)
    {
    }

    public DbSet<Station> Stations => Set<Station>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Station>(entity =>
        {
            entity.ToTable("Stations");
            entity.HasKey(x => x.StationId);
            entity.Property(x => x.StationId).ValueGeneratedNever();
            entity.Property(x => x.CityId).IsRequired();
            entity.Property(x => x.OccupancyDensity).IsRequired();
        });
    }
}


