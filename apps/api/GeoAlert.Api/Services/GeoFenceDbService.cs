using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;

using GeoAlert.Api.Models;
using System.Threading.Tasks;

namespace GeoAlert.Api.Services;


    public class GeoFenceDbService : IGeoFenceDbService
    {   
        private readonly IDynamoDBContext _db;

        public GeoFenceDbService(IDynamoDBContext db)
        {
            _db = db;
        }

        public Task SaveAsync(GeoFenceDto item)
            => _db.SaveAsync(item);

        public async Task<GeoFenceDto> GetByIdAsync(Guid id)
            => await _db.LoadAsync<GeoFenceDto>(id);

        public async Task<IEnumerable<GeoFenceDto>> GetAllAsync()
        {
            var conditions = new List<ScanCondition>();
            return await _db.ScanAsync<GeoFenceDto>(conditions).GetRemainingAsync();
        }
    }
    