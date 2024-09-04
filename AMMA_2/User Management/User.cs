using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AMMAAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        public string? Name { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }

        public string? ContactNumber { get; set; }

        public string? Role { get; set; }

        public string? Token { get; set; }

        public DateTime LastActive { get; set; }
    }
}
