namespace TravelNow.Business.Models.Hotel;

public class HotelResponseDetailDto
{
    /// <summary>
    /// Nombre
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Pais
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Ciudad
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Define si está disponible
    /// </summary>
    public bool IsEnabled { get; set; }
}
