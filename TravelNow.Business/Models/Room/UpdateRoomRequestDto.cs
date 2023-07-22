
namespace TravelNow.Business.Models.Room;

public class UpdateRoomRequestDto : RoomRequestBaseDto
{
    /// <summary>
    /// Id de habitación.
    /// </summary>
    public Guid Id { get; set; }
}
