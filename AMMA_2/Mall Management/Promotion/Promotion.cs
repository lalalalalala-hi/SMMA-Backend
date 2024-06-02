using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Globalization;

namespace AMMAAPI.Models
{
    public class Promotion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PromotionId { get; set; }
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
