using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace AMMAAPI.Models
{
    public class Routes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RouteId { get; set; }

        public string startRoute { get; set; }

        public string endRoute { get; set; }

        public int count { get; set; }
    }
}
