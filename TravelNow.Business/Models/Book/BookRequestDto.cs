
using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.Book;

public class BookRequestDto
{

    /// <summary>
    /// Id de reserva
    /// </summary>
    public string? BookId { get; set; }

    /// <summary>
    /// Id de hotel
    /// </summary>
    [Required]
    public string? HotelId { get; set; }

    /// <summary>
    /// Id de habitación
    /// </summary>
    [Required]
    public string? RoomId { get; set; }

    /// <summary>
    /// Id Cliente
    /// </summary>
    [Required]
    public string? PassengerId { get; set; }

    /// <summary>
    /// Define si está disponible
    /// </summary>
    [Required]
    public UInt16 PeopleNumber { get; set; }

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
}
