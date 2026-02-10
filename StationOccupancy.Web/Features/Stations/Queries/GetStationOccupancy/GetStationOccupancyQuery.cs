using MediatR;

namespace StationOccupancy.Web.Features.Stations.Queries.GetStationOccupancy;

public sealed record GetStationOccupancyQuery(int StationId) : IRequest<OccupancyDensityResponse>;


