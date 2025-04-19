using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceSample.CommandService.Domains;

public class Command
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("HowTo")]
    public string HowTo { get; set; } = string.Empty;

    [BsonElement("CommandLine")]
    public string CommandLine { get; set; } = string.Empty;

    [BsonElement("PlatformId")]
    public string PlatformId { get; set; } = string.Empty;

    [BsonIgnore]
    public virtual Platform Platform { get; set; } = null!;
}
