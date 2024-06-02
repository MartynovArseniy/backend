using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Measurement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string DeviceId { get; set; }
    public string MeasurementType { get; set; }
    public string Value { get; set; }
}