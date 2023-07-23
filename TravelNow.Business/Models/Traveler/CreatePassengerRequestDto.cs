using System.ComponentModel.DataAnnotations;
using TravelNow.Common.Enums;

namespace TravelNow.Business.Models.Traveler;

public class CreatePassengerRequestDto
{
    /// <summary>
    /// Nombre completo
    /// </summary>
    [Required]
    public string? FullName { get; set; }
    
    /// <summary>
    /// Fecha de nacimiento
    /// </summary>
    [Required]
    public DateTime BornDate { get; set; }

    /// <summary>
    /// Genero
    /// 0 - Masculino
    /// 1 - Femenico
    /// 2 - Otro
    /// </summary>
    [Required]
    public EGender Gender { get; set; }

    /// <summary>
    /// Tipo de documento
    /// 0 - CC (Cedula ciudadania)
    /// 1 - TI (Tarjeta Identidad)
    /// 2 - CE (Cedula extrangeria)
    /// 3 - PP (Pasaporte)
    /// </summary>
    [Required]
    public EIdentificationType IdentificationType { get; set; }

    /// <summary>
    /// Número de identificación
    /// </summary>
    [Required]
    public string? IdentificationNumber { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    /// <summary>
    /// Número telefonico
    /// </summary>
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? PhoneNumber { get; set; }
}
