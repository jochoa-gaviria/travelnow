namespace TravelNow.Business.Models.Hotel;

public class FindHotelResponseDto
{
    /// <summary>
    /// Lista de hoteles disponibles
    /// </summary>
    public List<FindHotel>? Hotels { get; set; } 
}

public class FindHotel
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
}
