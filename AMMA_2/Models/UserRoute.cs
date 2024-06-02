using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AMMAAPI.Models
{
    public class UserRoute
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserRouteId { get; set; }

        public string UserId { get; set; }

        public string StoreId { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}
