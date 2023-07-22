
namespace TravelNow.Business.Models.Room;

public class DisableRoomRequestDto
{
    /// <summary>
    /// Id de habitación
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Determina si esta habilitado o deshabilitado
    /// </summary>
    public bool IsEnabled { get; set; }
}
