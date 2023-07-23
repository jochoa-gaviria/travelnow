
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.Room;

public class DisableRoomRequestDto
{
    /// <summary>
    /// Id de habitación
    /// </summary>
    [Required]
    public string? Id { get; set; }

    /// <summary>
    /// Determina si esta habilitado o deshabilitado
    /// </summary>
    [Required]
    public bool IsEnabled { get; set; }
}
