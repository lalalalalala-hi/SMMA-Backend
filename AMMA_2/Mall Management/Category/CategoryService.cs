using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AMMAAPI.Database;
using AMMAAPI.Models;

namespace AMMAAPI.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _category;

        public CategoryService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _category = database.GetCollection<Category>(settings.Value.CollectionName.Category);
        }

        public async Task<List<Category>> GetAsync() =>
            await _category.Find(_=> true).ToListAsync();

        public async Task<Category> GetByIdAsync (string id)
        {
               var c = Builders<Category>.Filter.Eq("CategoryId", id);
            return await _category.Find(c).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Category c) =>
            await _category.InsertOneAsync(c);

        public async Task UpdateAsync(Category cIn)
        {
               var c = Builders<Category>.Filter.Eq("CategoryId", cIn.CategoryId);
            await _category.ReplaceOneAsync(c, cIn);
        }

        public async Task RemoveAsync(Category cIn) =>
            await _category.DeleteOneAsync(c => c.CategoryId == cIn.CategoryId);
    }
}
