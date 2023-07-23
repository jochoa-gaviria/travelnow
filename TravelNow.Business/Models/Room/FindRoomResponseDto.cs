
namespace TravelNow.Business.Models.Room;

public class FindRoomResponseDto
{
    public List<FindRoom> Rooms { get; set; } 
}

public class FindRoom
{
    public string? RoomNumber { get; set; }
    public float BaseCost { get; set; }
    public float Tax { get; set; }
    public Int32 RoomType { get; set; }
}
