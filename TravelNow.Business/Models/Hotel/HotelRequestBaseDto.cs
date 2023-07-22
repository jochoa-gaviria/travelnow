namespace TravelNow.Business.Models.HotelRequest;

public class HotelRequestBaseDto
{
    /// <summary>
    /// Nombre Hotel
    /// </summary>
    /// <example>Continental</example>
    public string? Name { get; set; }

    /// <summary>
    /// Pais
    /// </summary>
    /// <example>Colombia</example>
    public string? Country { get; set; }

    /// <summary>
    /// Ciudad
    /// </summary>
    /// <example>Medellin</example>
    public string? City { get; set; }
}
