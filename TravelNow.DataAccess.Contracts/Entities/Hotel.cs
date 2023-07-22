
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelNow.DataAccess.Contracts.Entities;

public class Hotel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public bool IsEnabled { get; set; }
}
