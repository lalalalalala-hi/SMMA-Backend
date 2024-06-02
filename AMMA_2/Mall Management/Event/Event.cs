using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AMMAAPI.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String EventId { get; set; }

        public string StoreId { get; set; }
        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string StartDate { get; set; }

        public string StartTime { get; set; }

        public string EndDate { get; set; }

        public string EndTime { get; set; }
    }
}
