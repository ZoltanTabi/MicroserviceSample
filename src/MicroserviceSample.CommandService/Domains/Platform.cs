using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceSample.CommandService.Domains;

public class Platform
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("ExternalId")]
    public int ExternalId { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("Commands")]
    public ICollection<Command> Commands { get; set; } = [];
}
