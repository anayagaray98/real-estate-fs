using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstateAPI.Models
{
    public class Owner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? IdOwner { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("address")]
        public string? Address { get; set; } = null!;

        [BsonElement("photo")]
        public string? Photo { get; set; } = null!;

        [BsonElement("birthday")]
        public DateTime? Birthday { get; set; }
    }
}
