using System.ComponentModel.DataAnnotations;
using TravelNow.Common.Enums;

namespace TravelNow.Business.Models.Room;

public class RoomRequestBaseDto
{
    /// <summary>
    /// Numero de habitación
    /// </summary>
    public string? RoomNumber { get; set; }

    /// <summary>
    /// Id de hotel
    /// </summary>
    [Required]
    public string? HotelId { get; set; }
    
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
}
