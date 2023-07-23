
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.Room;

public class UpdateRoomRequestDto : RoomRequestBaseDto
{
    /// <summary>
    /// Id de habitación.
    /// </summary>
    [Required]
    public string? Id { get; set; }
}
