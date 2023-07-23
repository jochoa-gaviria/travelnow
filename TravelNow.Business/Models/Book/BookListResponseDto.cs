using System.ComponentModel.DataAnnotations;

namespace TravelNow.Business.Models.Book;

public class BookListResponseDto
{
    /// <summary>
    /// Lista de reservas
    /// </summary>
    public List<BookResponse>? Books { get; set; }
}

public class BookResponse
{
    /// <summary>
    /// Id de reserva
    /// </summary>
    public string? BookId { get; set; }

    /// <summary>
    /// Id de hotel
    /// </summary>
    public string? HotelId { get; set; }

    /// <summary>
    /// Id de habitación
    /// </summary>
    public string? RoomId { get; set; }

    /// <summary>
    /// Id Cliente
    /// </summary>
    public string? PassengerId { get; set; }

    /// <summary>
    /// Define si está disponible
    /// </summary>
    public UInt16 PeopleNumber { get; set; }

    /// <summary>
    /// Fecha inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha fin
    /// </summary>
    public DateTime EndDate { get; set; }
}