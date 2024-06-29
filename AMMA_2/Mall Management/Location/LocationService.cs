using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AMMAAPI.Database;
using AMMAAPI.Models;

namespace AMMAAPI.Services
{
    public class LocationService
    {
        private readonly IMongoCollection<Location> _location;

        public LocationService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _location = database.GetCollection<Location>(settings.Value.CollectionName.Location);
        }

        public async Task<List<Location>> GetAsync() =>
            await _location.Find(_ => true).ToListAsync();

        public async Task<Location> GetByIdAsync(string id)
        {
            var l = Builders<Location>.Filter.Eq("LocationId", id);
            return await _location.Find(l).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Location l) =>
            await _location.InsertOneAsync(l);

        public async Task UpdateAsync(Location lIn)
        {
            var l = Builders<Location>.Filter.Eq("LocationId", lIn.LocationId);
            await _location.ReplaceOneAsync(l, lIn);
        }

        public async Task RemoveAsync(Location lIn) =>
            await _location.DeleteOneAsync(l => l.LocationId == lIn.LocationId);

        internal void Remove(Task<Location> l)
        {
            throw new NotImplementedException();
        }
    }
}
