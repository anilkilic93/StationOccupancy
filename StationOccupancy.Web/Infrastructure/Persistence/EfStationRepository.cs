using Microsoft.EntityFrameworkCore;
using StationOccupancy.Web.Domain.Stations;

namespace StationOccupancy.Web.Infrastructure.Persistence;

public sealed class EfStationRepository : IStationReadRepository, IStationWriteRepository
{
    private readonly StationOccupancyDbContext _db;

    public EfStationRepository(StationOccupancyDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Station station, CancellationToken cancellationToken)
    {
        var exists = await _db.Stations.AsNoTracking()
            .AnyAsync(x => x.StationId == station.StationId, cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException($"Station '{station.StationId}' already exists.");
        }

        _db.Stations.Add(station);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task<Station?> GetByIdAsync(int stationId, CancellationToken cancellationToken)
    {
        return _db.Stations.AsNoTracking()
            .FirstOrDefaultAsync(x => x.StationId == stationId, cancellationToken);
    }
}


