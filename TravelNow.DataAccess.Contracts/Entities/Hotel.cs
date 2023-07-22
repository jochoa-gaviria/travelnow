
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelNow.DataAccess.Contracts.Entities;

public class Hotel
{
    /// <summary>
    /// Id hotel
    /// </summary>
    [BsonId]
    public ObjectId Id { get; set; }
    
    /// <summary>
    /// Nombre
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Pais
    /// </summary>
    public string? Country { get; set; }
    
    /// <summary>
    /// Ciudad
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Define si está disponible
    /// </summary>
    public bool IsEnabled { get; set; }
}
