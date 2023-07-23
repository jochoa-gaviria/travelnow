using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.Hotel;

public class FindHotelRequestDto
{
    /// <summary>
    /// Fecha inicio
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha fin
    /// </summary>
    [Required]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Cantidad de personas entre 0 y 65535
    /// </summary>
    [Required]
    public UInt16 PeopleNumber { get; set; }

    /// <summary>
    /// Ciudad
    /// </summary>
    [Required]
    public string? City { get; set; }
}
