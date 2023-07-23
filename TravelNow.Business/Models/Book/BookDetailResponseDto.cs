using TravelNow.Business.Models.Hotel;
using TravelNow.Business.Models.Room;
using TravelNow.Business.Models.Traveler;

namespace TravelNow.Business.Models.Book;

public class BookDetailResponseDto
{

    /// <summary>
    /// Id de reserva
    /// </summary>
    public string? BookId { get; set; }

    /// <summary>
    /// Hotel
    /// </summary>
    public HotelResponseDetailDto? Hotel { get; set; }

    /// <summary>
    /// habitación
    /// </summary>
    public RoomResponseDetailDto? Room { get; set; }

    /// <summary>
    /// Pasajero
    /// </summary>
    public PassengerResponseDetailDto? Passenger { get; set; }

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


