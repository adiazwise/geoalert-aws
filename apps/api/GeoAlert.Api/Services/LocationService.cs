using Amazon.LocationService;
using Amazon.LocationService.Model;

namespace GeoAlert.Api.Services;

public class LocationService : ILocationService
{
    private readonly IAmazonLocationService _locationService;
    private readonly IConfiguration _configuration;

    public LocationService(IAmazonLocationService locationService, IConfiguration configuration)
    {
        _locationService = locationService;
        _configuration = configuration;
    }
    public async Task<(double Latitude, double Longitude)> GetCoordinateAsync(string address)
    {
        var indexName = Environment.GetEnvironmentVariable("PLACE_INDEX_NAME") ?? throw new ArgumentNullException("IndexName", "Place Index name is not configured.");
        var response = await _locationService.SearchPlaceIndexForTextAsync(new SearchPlaceIndexForTextRequest()
        {
            IndexName = indexName,
            Text = address,
            MaxResults = 1
        });
        
        double latitude = 0, longitude = 0;

        if (response.Results != null && response.Results.Count > 0)
        {
            var position = response.Results[0].Place.Geometry.Point;
            latitude = position[0];
            longitude = position[1];
        }
        return (latitude, longitude);
        
    }
}