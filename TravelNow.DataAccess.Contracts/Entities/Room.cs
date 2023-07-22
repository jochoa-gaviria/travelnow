using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TravelNow.Common.Enums;

namespace TravelNow.DataAccess.Contracts.Entities;

public class Room
{
    /// <summary>
    /// Id de habitación
    /// </summary>
    [BsonId]
    public ObjectId Id { get; set; }

    /// <summary>
    /// Numero de habitación
    /// </summary>
    public string? RoomNumber { get; set; }

    /// <summary>
    /// Id de hotel
    /// </summary>
    public ObjectId HotelId { get; set; }

    /// <summary>
    /// Costo base de habitación
    /// </summary>
    public float BaseCost { get; set; }

    /// <summary>
    /// Impuestos
    /// </summary>
    public float Tax { get; set; }

    /// <summary>
    /// Tipo de habitación
    /// 0 - Suite
    /// 1 - Suite Junior
    /// 2 - Gran Suite
    /// 3 - Individual
    /// 4 - Doble
    /// </summary>
    public RoomTypes RoomType { get; set; }


    /// <summary>
    /// Define si esta disponible
    /// </summary>
    public bool IsEnabled { get; set; }
}
