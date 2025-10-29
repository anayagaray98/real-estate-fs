using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstateAPI.Models
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? IdPropertyImage { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("idProperty")]
        public string IdProperty { get; set; } = null!; // Property relationship

        [BsonElement("file")]
        public string File { get; set; } = null!;

        [BsonElement("enabled")]
        public bool Enabled { get; set; }
    }
}
