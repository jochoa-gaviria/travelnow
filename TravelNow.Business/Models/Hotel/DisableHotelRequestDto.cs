using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.HotelRequest;

public class DisableHotelRequestDto
{
    /// <summary>
    /// Id de hotel
    /// </summary>
    [Required]
    public string? Id { get; set; }

    /// <summary>
    /// Determina si esta habilitado o deshabilitado
    /// </summary>
    [Required]
    public bool IsEnabled { get; set; }
}
