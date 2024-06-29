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

        public string RouteId { get; set; }

        public string RouteTime { get; set; }

    }
}
