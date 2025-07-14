namespace GeoAlert.Api.Services;

public interface ILocationService
{
    Task<(double Latitude, double Longitude)> GetCoordinateAsync(string address);
}