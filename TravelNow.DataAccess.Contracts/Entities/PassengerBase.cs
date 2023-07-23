
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TravelNow.DataAccess.Contracts.Entities;

public class PassengerBase
{
    [BsonId]
    public ObjectId Id { get; set; }
    /// <summary>
    /// Nombre completo
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Número telefonico
    /// </summary>
    public string? PhoneNumber { get; set; }
}
