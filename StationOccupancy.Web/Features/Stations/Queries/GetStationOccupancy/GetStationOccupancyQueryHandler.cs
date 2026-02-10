using MediatR;
using StationOccupancy.Web.Infrastructure.Persistence;

namespace StationOccupancy.Web.Features.Stations.Queries.GetStationOccupancy;

public sealed class GetStationOccupancyQueryHandler : IRequestHandler<GetStationOccupancyQuery, OccupancyDensityResponse>
{
    private readonly IStationReadRepository _readRepository;

    public GetStationOccupancyQueryHandler(IStationReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<OccupancyDensityResponse> Handle(GetStationOccupancyQuery request, CancellationToken cancellationToken)
    {
        var station = await _readRepository.GetByIdAsync(request.StationId, cancellationToken);
        if (station is null)
        {
            throw new KeyNotFoundException($"Station '{request.StationId}' not found.");
        }

        return new OccupancyDensityResponse(station.OccupancyDensity);
    }
}


