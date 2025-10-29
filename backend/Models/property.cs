using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstateAPI.Models
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? IdProperty { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("address")]
        public string Address { get; set; } = null!;

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("codeInternal")]
        public string CodeInternal { get; set; } = null!;

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("idOwner")] // Owner relationship
        public string IdOwner { get; set; } = null!;
    }
}