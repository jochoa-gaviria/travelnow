using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.Traveler;

public class CreatePassengerEmergencyContactRequestDto
{
    /// <summary>
    /// Nombre completo
    /// </summary>
    [Required]
    public string? FullName { get; set; }

    /// <summary>
    /// Número de telefono
    /// </summary>
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? PhoneNumber { get; set; }


    /// <summary>
    /// Id de pasajero
    /// </summary>
    [Required]
    public string? PassengerId { get; set; }    
}
