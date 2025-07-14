using GeoAlert.Api.Models;

  public interface IGeoFenceDbService
    {
        Task SaveAsync(GeoFenceDto item);
        Task<GeoFenceDto> GetByIdAsync(Guid id);
        Task<IEnumerable<GeoFenceDto>> GetAllAsync();
    }