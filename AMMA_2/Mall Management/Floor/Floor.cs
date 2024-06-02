using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AMMAAPI.Models
{
    public class Floor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FloorId { get; set; }

        public string FloorName { get; set; }

    }
}
