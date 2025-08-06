using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.LocationService;

namespace GeoAlert.Api.Services;

public static class GeofencesServices
{
    public static void AddGeofencesServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddAWSService<IAmazonDynamoDB>();

        services.AddAWSService<IAmazonLocationService>();
        services.AddSingleton<ILocationService, LocationService>();
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        services.AddScoped<IGeoFenceDbService, GeoFenceDbService>();

        // Add other services as needed
    
       
        services.AddRouting(options => options.LowercaseUrls = true);
    }
    
}