using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MongoExample.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement]
        [BsonRequired]
        public string code { get; set; }

        [BsonElement]
        [BsonIgnoreIfNull]
        public string name { get; set; }

        [BsonElement]
        [BsonDefaultValue(0)]
        public int status { get; set; }

        [BsonElement]
        [BsonIgnoreIfNull]
        public int price { get; set; }

        [BsonElement("tag")]
        [JsonPropertyName("tag")]
        public List<string> tagList { get; set; } = null!;

        [BsonElement]
        [BsonIgnoreIfNull]
        public DateTime? create_time { get; set; } = DateTime.UtcNow;

        [BsonElement]
        [BsonIgnoreIfNull]
        public DateTime? update_time { get; set; } = DateTime.UtcNow;
    }
}
