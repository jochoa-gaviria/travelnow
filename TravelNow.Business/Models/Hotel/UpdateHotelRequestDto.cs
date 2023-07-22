using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.HotelRequest;

public class UpdateHotelRequestDto : HotelRequestBaseDto
{
    /// <summary>
    /// Id de hotel
    /// </summary>
    [Required]
    public string? Id { get; set; }
}
