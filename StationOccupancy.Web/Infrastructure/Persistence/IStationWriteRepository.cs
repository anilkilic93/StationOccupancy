using StationOccupancy.Web.Domain.Stations;

namespace StationOccupancy.Web.Infrastructure.Persistence;

public interface IStationWriteRepository
{
    Task AddAsync(Station station, CancellationToken cancellationToken);
}


