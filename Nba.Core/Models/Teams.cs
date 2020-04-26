using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nba.Core
{
    public class Teams
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("teamId")]
        public long TeamId { get; set; }

        [BsonElement("abbreviation")]
        public string Abbreviation { get; set; }

        [BsonElement("teamName")]
        public string TeamName { get; set; }

        [BsonElement("simpleName")]
        public string SimpleName { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }
    }
}