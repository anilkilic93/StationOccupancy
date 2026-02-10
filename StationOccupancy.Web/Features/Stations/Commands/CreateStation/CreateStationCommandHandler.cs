using MediatR;
using StationOccupancy.Web.Domain.Stations;
using StationOccupancy.Web.Infrastructure.Persistence;

namespace StationOccupancy.Web.Features.Stations.Commands.CreateStation;

public sealed class CreateStationCommandHandler : IRequestHandler<CreateStationCommand, int>
{
    private readonly IStationWriteRepository _writeRepository;

    public CreateStationCommandHandler(IStationWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task<int> Handle(CreateStationCommand request, CancellationToken cancellationToken)
    {
        var station = new Station
        {
            StationId = request.StationId,
            CityId = request.CityId,
            OccupancyDensity = 0
        };

        await _writeRepository.AddAsync(station, cancellationToken);
        return station.StationId;
    }
}


