using AMMAAPI.Database;
using AMMAAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AMMAAPI.Services
{
    public class EventsService
    {
        private readonly IMongoCollection<Event> _event;

        public EventsService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _event = database.GetCollection<Event>(settings.Value.CollectionName.Event);
        }


        public async Task<List<Event>> GetAsync() =>
            await _event.Find(_=> true).ToListAsync();

        public async Task<Event> GetByIdAsync (string id){
            var e = Builders<Event>.Filter.Eq("EventId", id);
            return await _event.Find(e).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Event e) =>
            await _event.InsertOneAsync(e);

        public async Task UpdateAsync(Event eIn){
            var e = Builders<Event>.Filter.Eq("EventId", eIn.EventId);
            await _event.ReplaceOneAsync(e, eIn);
        }

        public async Task RemoveAsync(Event eIn) =>
            await _event.DeleteOneAsync(e => e.EventId == eIn.EventId);

        internal void Remove(Task<Event> e)
        {
            throw new NotImplementedException();
        }
    }
}
