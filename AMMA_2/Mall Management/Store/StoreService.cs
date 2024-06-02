using AMMAAPI.Database;
using AMMAAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AMMAAPI.Services
{
    public class StoreService
    {
        private readonly IMongoCollection<Store> _store;

        public StoreService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _store = database.GetCollection<Store>(settings.Value.CollectionName.Store);
        }

        public async Task<List<Store>> GetAsync() =>
            await _store.Find(_=> true).ToListAsync();

        public async Task<Store> GetByIdAsync (string id){
            var s = Builders<Store>.Filter.Eq("StoreId", id);
            return await _store.Find(s).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Store s) =>
            await _store.InsertOneAsync(s);

        public async Task UpdateAsync(Store sIn){
            var s = Builders<Store>.Filter.Eq("StoreId", sIn.StoreId);
            await _store.ReplaceOneAsync(s, sIn);
        }

        public async Task RemoveAsync(Store sIn) =>
            await _store.DeleteOneAsync(s => s.StoreId == sIn.StoreId);

        internal void Remove(Task<Store> s)
        {
            throw new NotImplementedException();
        }
    }
}
