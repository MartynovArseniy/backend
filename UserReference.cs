using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

//public class UserReference
//{
//    [BsonRepresentation(BsonType.ObjectId)]
//    public string Id { get; set; }
//}


public class UserReference
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
   
}
