namespace TravelNow.Business.Models.Hotel;

public class CreateHotelResponseDto : ResponseBaseDto
{
    /// <summary>
    /// Id generado del hotel
    /// </summary>
    public Guid Id { get; set; }
}
