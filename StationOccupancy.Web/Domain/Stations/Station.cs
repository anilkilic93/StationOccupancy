namespace StationOccupancy.Web.Domain.Stations;

public sealed class Station
{
    public int StationId { get; set; }
    public int CityId { get; set; }

    // For now this is a simple scalar to bootstrap the CQRS flow.
    public int OccupancyDensity { get; set; }
}


