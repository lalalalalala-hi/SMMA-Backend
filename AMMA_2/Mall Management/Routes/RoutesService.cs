using AMMAAPI.Database;
using AMMAAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AMMAAPI.Services
{
    public class RoutesService
    {
        private readonly IMongoCollection<Routes> _route;

        public RoutesService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _route = database.GetCollection<Routes>(settings.Value.CollectionName.Routes);
        }

        public async Task<Routes> GetRouteByStartAndEndAsync(string startRoute, string endRoute) =>
        await _route.Find(r => r.startRoute == startRoute && r.endRoute == endRoute).FirstOrDefaultAsync();

        public async Task<List<Routes>> GetAsync() =>
            await _route.Find(_ => true).ToListAsync();

        public async Task<Routes> GetByIdAsync(string id)
        {
            var r = Builders<Routes>.Filter.Eq("RouteId", id);
            return await _route.Find(r).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Routes r) =>
            await _route.InsertOneAsync(r);

        public async Task UpdateAsync(Routes rIn)
        {
            var r = Builders< Routes>.Filter.Eq("RouteId", rIn.RouteId);
            await _route.ReplaceOneAsync(r, rIn);
        }

        public async Task RemoveAsync(Routes rIn) =>
            await _route.DeleteOneAsync(r => r.RouteId == rIn.RouteId);

        internal void Remove(Task<Routes> r)
        {
            throw new NotImplementedException();
        }


    }
}
