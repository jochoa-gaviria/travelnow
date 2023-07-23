using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.Hotel;

public class FindHotelRequestDto
{
    /// <summary>
    /// Ciudad
    /// </summary>
    [Required]
    public string? City { get; set; }
}
