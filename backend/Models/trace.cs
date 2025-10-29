using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstateAPI.Models
{
    public class PropertyTrace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? IdPropertyTrace { get; set; }

        [BsonElement("dateSale")]
        public DateTime DateSale { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("value")]
        public decimal Value { get; set; }

        [BsonElement("tax")]
        public decimal Tax { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("idProperty")]
        public string IdProperty { get; set; } = null!; // Property relationship
    }
}
