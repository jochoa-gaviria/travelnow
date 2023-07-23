using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models.Book;
using TravelNow.Business.Models.Hotel;
using TravelNow.Business.Models.Room;
using TravelNow.Business.Models.Traveler;
using TravelNow.Common.Enums;
using TravelNow.Common.Helpers;
using TravelNow.Common.Interfaces;
using TravelNow.Common.Models;
using TravelNow.DataAccess.Contracts.Entities;
using TravelNow.DataAccess.Contracts.Interfaces;

namespace TravelNow.Application.Services;

public class BookingService : IBookingService
{

    #region internals
    private readonly IEmailSenderService _emailSenderService;
    private readonly IDataBaseContextRepository _dataBaseContextRepository;
    private readonly ICollectionNameHelper _collectionNameHelper;
    private readonly string _hotelCollectionName;
    private readonly string _roomCollectionName;
    private readonly string _passengerCollectionName;
    private readonly string _bookCollectionName;

    #endregion internals

    #region constructor
    public BookingService(IDataBaseContextRepository dataBaseContextRepository, ICollectionNameHelper collectionNameHelper, IEmailSenderService emailSenderService)
    {
        _dataBaseContextRepository = dataBaseContextRepository;
        _collectionNameHelper = collectionNameHelper;
        _bookCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.book];
        _passengerCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.passerger];
        _roomCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.room];
        _hotelCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.hotel];
        _emailSenderService = emailSenderService;
    }
    #endregion contructor

    #region methods

    public async Task<ResponseDto<BookResponseDto>> Book(BookRequestDto bookRequestDto)
    {
        ResponseDto<BookResponseDto> serviceResponse = new();
        Passenger passenger = new Passenger();
        Book result = new Book();
        try
        {
            //Existencia hotel
            var hotelExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Hotel>(_hotelCollectionName, bookRequestDto?.HotelId);
            if (!hotelExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El hotel con id {bookRequestDto.HotelId} no existe.");
                return serviceResponse;
            }

            //Existencia habitación
            var room = await _dataBaseContextRepository.GetFirstDocumentByIdAsync<Room>(_roomCollectionName, bookRequestDto?.RoomId);
            if (room == null || room?.HotelId != new ObjectId(bookRequestDto.HotelId))
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"La habitación con id {bookRequestDto.RoomId} no existe o no corresponde al hotel.");
                return serviceResponse;
            }

            //Existencia cliente
            passenger = await _dataBaseContextRepository.GetFirstDocumentByIdAsync<Passenger>(_passengerCollectionName, bookRequestDto?.PassengerId);
            if (passenger == null)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El pasajero con id {bookRequestDto.PassengerId} no existe.");
                return serviceResponse;
            }

            //Habitación disponible en las fechas
            FilterDefinition<Book> startDateFilter = Builders<Book>.Filter.Lte(b => b.StartDate, bookRequestDto.StartDate);
            FilterDefinition<Book> endDateFilter = Builders<Book>.Filter.Gte(b => b.EndDate, bookRequestDto.EndDate);
            FilterDefinition<Book> roomIdFilter = Builders<Book>.Filter.Eq(b => b.RoomId, new ObjectId(bookRequestDto.RoomId));
            var filter = startDateFilter & endDateFilter & roomIdFilter;

            var enabledRoom = await _dataBaseContextRepository.GetFirstDocumentByFilterAsync(_bookCollectionName, filter);
            if (enabledRoom != null)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"La habitación {bookRequestDto.RoomId} se encuentra ocupada para la fecha indicada.");
                return serviceResponse;
            }

            var newBook = bookRequestDto.TMapper<Book>();
            newBook.RoomId = new ObjectId(bookRequestDto.RoomId);
            newBook.PassengerId = new ObjectId(bookRequestDto.PassengerId);
            newBook.HotelId = new ObjectId(bookRequestDto.HotelId);

            result = await _dataBaseContextRepository.InsertDocumentAsync(_bookCollectionName, newBook);
            serviceResponse.Response = new BookResponseDto
            {
                BookId = result.Id.ToString()
            };

            //Notificar por email asincronamente
            await Task.Factory.StartNew(() => SendEmailNotification(result, passenger)).ConfigureAwait(false);

        }
        catch (Exception)
        {
            serviceResponse.SetProperties(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado. Intentelo nuevamente.");
            return serviceResponse;
        }

        return serviceResponse;
    }

    public async Task<ResponseDto<BookDetailResponseDto>> BookDetail(string id)
    {
        ResponseDto<BookDetailResponseDto> serviceResponse = new();
        try
        {
            var book = await _dataBaseContextRepository.GetFirstDocumentByIdAsync<Book>(_bookCollectionName, id);
            if (book == null)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"La reserva con id {id} no existe.");
                return serviceResponse;
            }

            var hotel = await _dataBaseContextRepository.GetFirstDocumentByIdAsync<Hotel>(_hotelCollectionName, book.HotelId.ToString());
            if (hotel == null)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El hotel con id {book.HotelId} no existe.");
                return serviceResponse;
            }

            var room = await _dataBaseContextRepository.GetFirstDocumentByIdAsync<Room>(_roomCollectionName, book.RoomId.ToString());
            if (hotel == null)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"La habitación con id {book.RoomId} no existe.");
                return serviceResponse;
            }


            var passenger = await _dataBaseContextRepository.GetFirstDocumentByIdAsync<Passenger>(_passengerCollectionName, book.PassengerId.ToString());
            if (hotel == null)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El pasajero con id {book.PassengerId} no existe.");
                return serviceResponse;
            }


            serviceResponse.Response = book.TMapper<BookDetailResponseDto>();
            serviceResponse.Response.BookId = book.Id.ToString();
            serviceResponse.Response.Hotel = hotel.TMapper<HotelResponseDetailDto>();
            serviceResponse.Response.Room = room.TMapper<RoomResponseDetailDto>();
            serviceResponse.Response.Passenger = passenger.TMapper<PassengerResponseDetailDto>();
        }
        catch (Exception)
        {
            serviceResponse.SetProperties(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado. Intentelo nuevamente.");
            return serviceResponse;
        }
        return serviceResponse;
    }

    public async Task<ResponseDto<BookListResponseDto>> ListBook()
    {
        ResponseDto<BookListResponseDto> serviceResponse = new();
        try
        {
            var books = await _dataBaseContextRepository.GetAllDocumentsInCollectionAsync<Book>(_bookCollectionName);
            if (books == null || !books.Any())
            {
                serviceResponse.SetProperties(HttpStatusCode.NotFound, $"No se encuentraron reservas");
                return serviceResponse;
            }

            serviceResponse.Response = new BookListResponseDto
            {
                Books = books.Select(b => 
                {
                    var book = b.TMapper<BookResponse>();
                    book.BookId = b.Id.ToString();
                    book.HotelId = b.HotelId.ToString();
                    book.RoomId = b.RoomId.ToString();
                    book.PassengerId = b.PassengerId.ToString();

                    return book;
                }).ToList()
            };

        }
        catch (Exception)
        {
            serviceResponse.SetProperties(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado. Intentelo nuevamente.");
            return serviceResponse;
        }
        return serviceResponse;
    }

    #endregion methods

    #region private methods
    private async Task SendEmailNotification(Book book, Passenger passenger)
    {
        if (book != null && passenger != null)
        {
            string subject = $"Nueva reserva en TravelNow - {passenger.FullName}";
            string body = $"Señor(a), {passenger.FullName}, le informamos de su reserva con id {book.Id} para {book.PeopleNumber} personas \n Reserva desde {book.StartDate} hasta {book.EndDate}";
            await _emailSenderService.SendEmail(subject, body, passenger.Email);
        }
    }

    #endregion private methods
}
