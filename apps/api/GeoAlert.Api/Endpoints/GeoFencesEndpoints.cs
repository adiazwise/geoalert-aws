using GeoAlert.Api.Models;
using GeoAlert.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoAlert.Api.Endpoints;

public static class GeoFencesEndpoints
{
    public static void AddMapGeoFences(this IEndpointRouteBuilder endpoints)
    {
         endpoints.MapGet("/health", () => "The GeoAlert API is running!");

  
            endpoints.MapPost("/geofences", 
            async ([FromServices]ILocationService locationService,
                    [FromServices]IGeoFenceDbService geofenceDbService,
                    [FromBody] ZoneRequestDto zoneRequest) => 
            {
                if (zoneRequest == null || string.IsNullOrEmpty(zoneRequest.Address) || string.IsNullOrEmpty(zoneRequest.RequestedBy))
                {
                    return Results.BadRequest("Direccion no valida.");
                }
                // Aquí deberías llamar a tu servicio de ubicación para obtener las coordenadas
                (double Latitude, double Longitude) coordinates;
                try
                {
                     coordinates = await locationService.GetCoordinateAsync(zoneRequest.Address);
                    if (coordinates.Latitude == 0 && coordinates.Longitude == 0)
                    {
                        return Results.BadRequest("No se pudieron obtener las coordenadas para la dirección proporcionada.");
                    }
                }
                catch (ArgumentNullException ex)
                {
                    return Results.BadRequest($"Error al obtener el Place Index: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest($"Error al obtener el Place Index: {ex.Message}");
                }
             
                try
                {
                    var geofence = new GeoFenceDto
                    {
                        Id = Guid.NewGuid(),
                        Address = zoneRequest.Address,
                        Latitude = coordinates.Latitude,
                        Longitude = coordinates.Longitude,
                        CreatedBy = zoneRequest.RequestedBy,
                        CreatedAt = DateTime.UtcNow
                    };

                    await geofenceDbService.SaveAsync(geofence);
                    return Results.Created($"/geofences/{geofence.Id}", geofence);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error al crear en registro en tabla geofences: {ex.Message}");
                }
                
            });

            endpoints.MapGet("/geofences",  async ([FromServices]IGeoFenceDbService geofenceDbService) => 
            {
                var geofences = await geofenceDbService.GetAllAsync();
                if (geofences == null || !geofences.Any())
                {
                    return Results.NotFound("No geofences found.");
                }
                return Results.Ok(geofences);
            });
            endpoints.MapGet("/geofences/{id}", async ([FromServices]IGeoFenceDbService geofenceDbService, Guid id) => 
            {
                var geofence = await geofenceDbService.GetByIdAsync(id);
                if (geofence == null)
                {
                    return Results.NotFound($"Geofence with ID {id} not found.");
                }
                return Results.Ok(geofence);
            });
    }
}