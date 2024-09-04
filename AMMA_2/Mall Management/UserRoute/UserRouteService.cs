using AMMAAPI.Database;
using AMMAAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AMMAAPI.Services
{
    public class UserRouteService
    {
        private readonly IMongoCollection<UserRoute> _userRoute;

        public UserRouteService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _userRoute = database.GetCollection<UserRoute>(settings.Value.CollectionName.UserRoute);
        }
        public async Task<List<UserRoute>> GetAsync() =>
            await _userRoute.Find(_ => true).ToListAsync();

        public async Task<UserRoute> GetByIdAsync(string id)
        {
            var r = Builders<UserRoute>.Filter.Eq("UserRouteId", id);
            return await _userRoute.Find(r).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(UserRoute r) =>
            await _userRoute.InsertOneAsync(r);

        public async Task UpdateAsync(UserRoute rIn)
        {
            var r = Builders<UserRoute>.Filter.Eq("UserRouteId", rIn.UserRouteId);
            await _userRoute.ReplaceOneAsync(r, rIn);
        }

        public async Task RemoveAsync(UserRoute rIn) =>
            await _userRoute.DeleteOneAsync(r => r.UserRouteId == rIn.UserRouteId);

        internal void Remove(Task<UserRoute> r)
        {
            throw new NotImplementedException();
        }
    }
}
