using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AMMAAPI.Database;
using AMMAAPI.Models;

namespace AMMAAPI.Services
{
    public class FloorService
    {
        private readonly IMongoCollection<Floor> _floor;

        public FloorService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _floor = database.GetCollection<Floor>(settings.Value.CollectionName.Floor);
        }

        public async Task<List<Floor>> GetAsync() =>
            await _floor.Find(_=> true).ToListAsync();

        public async Task<Floor> GetByIdAsync (string id){
               var f = Builders<Floor>.Filter.Eq("FloorId", id);
            return await _floor.Find(f).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Floor f) =>
            await _floor.InsertOneAsync(f);

        public async Task UpdateAsync(Floor fIn){
               var f = Builders<Floor>.Filter.Eq("FloorId", fIn.FloorId);
            await _floor.ReplaceOneAsync(f, fIn);
        }

        public async Task RemoveAsync(Floor fIn) =>
            await _floor.DeleteOneAsync(f => f.FloorId == fIn.FloorId);

        internal void Remove(Task<Floor> f){
            throw new NotImplementedException();
        }
    }
}
