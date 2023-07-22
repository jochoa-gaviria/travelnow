namespace TravelNow.Business.Models.HotelRequest;

public class DisableHotelRequestDto
{
    /// <summary>
    /// Id de hotel
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Determina si esta habilitado o deshabilitado
    /// </summary>
    public bool IsEnabled { get; set; }
}
