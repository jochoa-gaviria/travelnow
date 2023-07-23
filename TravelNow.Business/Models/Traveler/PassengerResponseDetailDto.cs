
using TravelNow.Common.Enums;

namespace TravelNow.Business.Models.Traveler;

public class PassengerResponseDetailDto
{
    /// <summary>
    /// Nombre completo
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Número telefonico
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Fecha de nacimiento
    /// </summary>
    public DateTime BornDate { get; set; }

    /// <summary>
    /// Genero
    /// 0 - Masculino
    /// 1 - Femenico
    /// 2 - Otro
    /// </summary>
    public EGender Gender { get; set; }

    /// <summary>
    /// Tipo de documento
    /// 0 - CC (Cedula ciudadania)
    /// 1 - TI (Tarjeta Identidad)
    /// 2 - CE (Cedula extrangeria)
    /// 3 - PP (Pasaporte)
    /// </summary>
    public EIdentificationType IdentificationType { get; set; }

    /// <summary>
    /// Número de identificación
    /// </summary>
    public string? IdentificationNumber { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }
}
