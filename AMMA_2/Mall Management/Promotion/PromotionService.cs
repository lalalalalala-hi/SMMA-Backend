using AMMAAPI.Database;
using AMMAAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AMMAAPI.Services
{
    public class PromotionService
    {
        private readonly IMongoCollection<Promotion> _promotions;

        public PromotionService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _promotions = database.GetCollection<Promotion>(settings.Value.CollectionName.Promotion);
        }

        public async Task<List<Promotion>> GetAsync() =>
            await _promotions.Find(_=> true).ToListAsync();

        public async Task<Promotion> GetByIdAsync (string id){
            var p = Builders<Promotion>.Filter.Eq("PromotionId", id);
            return await _promotions.Find(p).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Promotion p) =>
            await _promotions.InsertOneAsync(p);

        public async Task UpdateAsync(Promotion pIn){
            var p = Builders<Promotion>.Filter.Eq("PromotionId", pIn.PromotionId);
            await _promotions.ReplaceOneAsync(p, pIn);
        }

        public async Task RemoveAsync(Promotion pIn) =>
            await _promotions.DeleteOneAsync(p => p.PromotionId == pIn.PromotionId);

        internal void Remove(Task<Promotion> p)
        {
               throw new NotImplementedException();
        }
    }
}
