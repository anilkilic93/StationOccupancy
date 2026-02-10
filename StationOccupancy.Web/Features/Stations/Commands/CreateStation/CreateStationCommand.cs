using MediatR;

namespace StationOccupancy.Web.Features.Stations.Commands.CreateStation;

public sealed record CreateStationCommand(int StationId, int CityId) : IRequest<int>;


