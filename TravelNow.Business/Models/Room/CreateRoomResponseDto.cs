namespace TravelNow.Business.Models.Room;

public class CreateRoomResponseDto : ResponseBaseDto
{
    /// <summary>
    /// Id generado de la habitación
    /// </summary>
    public Guid Id { get; set; }
}
