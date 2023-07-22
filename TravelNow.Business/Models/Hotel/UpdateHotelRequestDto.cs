namespace TravelNow.Business.Models.HotelRequest;

public class UpdateHotelRequestDto : HotelRequestBaseDto
{
    /// <summary>
    /// Id de hotel
    /// </summary>
    public Guid Id { get; set; }
}
