using StationOccupancy.Web.Domain.Stations;

namespace StationOccupancy.Web.Infrastructure.Persistence;

public interface IStationReadRepository
{
    Task<Station?> GetByIdAsync(int stationId, CancellationToken cancellationToken);
}


