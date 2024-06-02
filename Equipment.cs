using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Equipment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string RoomId { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
}