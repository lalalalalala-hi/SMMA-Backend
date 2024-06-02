using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace AMMAAPI.Models
{
    public class Store
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string StoreId { get; set; }

        public string FloorId { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string ContactNumber { get; set; }

        public string OpeningTime { get; set; }

        public string ClosingTime { get; set; }

    }
}
