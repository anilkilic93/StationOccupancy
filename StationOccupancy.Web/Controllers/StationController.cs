using MediatR;
using Microsoft.AspNetCore.Mvc;
using StationOccupancy.Web.Features.Stations.Commands.CreateStation;
using StationOccupancy.Web.Features.Stations.Queries.GetStationOccupancy;

namespace StationOccupancy.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StationController : ControllerBase
{
    private readonly IMediator _mediator;

    public StationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(nameof(CreateStation))]
    public async Task<ActionResult<int>> CreateStation([FromBody] CreateStationCommand command, CancellationToken cancellationToken)
    {
        var stationId = await _mediator.Send(command, cancellationToken);
        return Ok(stationId);
    }

    [HttpGet($"{nameof(GetStationOccupancy)}/{{stationId:int}}")]
    public async Task<ActionResult<OccupancyDensityResponse>> GetStationOccupancy(int stationId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new GetStationOccupancyQuery(stationId), cancellationToken);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}


