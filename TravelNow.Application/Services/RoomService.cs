using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models;
using TravelNow.Business.Models.Room;
using TravelNow.Common.Enums;
using TravelNow.Common.Helpers;
using TravelNow.Common.Interfaces;
using TravelNow.Common.Models;
using TravelNow.DataAccess.Contracts.Entities;
using TravelNow.DataAccess.Contracts.Interfaces;

namespace TravelNow.Application.Services;

public class RoomService : IRoomService
{
    #region internals

    /// <summary>
    /// DataAcces repository
    /// </summary>
    private readonly IDataBaseContextRepository _dataBaseContextRepository;

    /// <summary>
    /// Helper para colección de base de datos
    /// </summary>
    private readonly ICollectionNameHelper _collectionNameHelper;

    /// <summary>
    /// Nombre de la colección de habitación de base de datos
    /// </summary>
    private readonly string _roomCollectionName;

    /// <summary>
    /// Nombre de la colección de hotel de base de datos
    /// </summary>
    private readonly string _hotelCollectionName;

    #endregion internals

    #region constructor
    public RoomService(IDataBaseContextRepository dataBaseContextRepository, ICollectionNameHelper collectionNameHelper)
    {
        _dataBaseContextRepository = dataBaseContextRepository;
        _collectionNameHelper = collectionNameHelper;
        _roomCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.room];
        _hotelCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.hotel];
    }
    #endregion constructor

    #region methods
    public async Task<ResponseDto<CreateRoomResponseDto>> CreateRoom(CreateRoomRequestDto createRoomRequestDto)
    {
        ResponseDto<CreateRoomResponseDto> serviceResponse = new();

        try
        {

            var hotelExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Hotel>(_hotelCollectionName, createRoomRequestDto?.HotelId);
            if (!hotelExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El hotel con id {createRoomRequestDto.HotelId} no existe.");
                return serviceResponse;
            }

            FilterDefinition<Room> filter = new BsonDocument(nameof(createRoomRequestDto.RoomNumber), createRoomRequestDto.RoomNumber);
            var roomAlreadyExists = await _dataBaseContextRepository.ExistsDocumentAsync(_roomCollectionName, filter);
            if (roomAlreadyExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"La habitación {createRoomRequestDto.RoomNumber} ya existe.");
                return serviceResponse;
            }

            if (!Enum.IsDefined(typeof(RoomTypes), createRoomRequestDto.RoomType))
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El tipo de habitación {createRoomRequestDto.RoomType} no es válido");
                return serviceResponse;
            }

            var newRoom = createRoomRequestDto.TMapper<Room>();
            newRoom.HotelId = new ObjectId(createRoomRequestDto.HotelId);
            newRoom.IsEnabled = true;

            var result = await _dataBaseContextRepository.InsertDocumentAsync(_roomCollectionName, newRoom);

            serviceResponse.Response = new CreateRoomResponseDto
            {
                Id = result.Id.ToString(),
                IsSuccess = true,
            };
        }
        catch (Exception)
        {
            serviceResponse.SetProperties(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado. Intentelo nuevamente.");
            return serviceResponse;
        }

        return serviceResponse;
    }

    public async Task<ResponseDto<ResponseBaseDto>> DisableRoom(DisableRoomRequestDto disableRoomRequestDto)
    {
        ResponseDto<ResponseBaseDto> serviceResponse = new();
        try
        {
            var alreadyExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Room>(_roomCollectionName, disableRoomRequestDto?.Id);
            if (!alreadyExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"La habitación con id {disableRoomRequestDto.Id} no existe.");
                return serviceResponse;
            }


            UpdateDefinition<Room> updateDefinition = Builders<Room>.Update.Set(room => room.IsEnabled, disableRoomRequestDto.IsEnabled);

            var result = await _dataBaseContextRepository.UpdateDocumentByIdAsync(_roomCollectionName, disableRoomRequestDto.Id, updateDefinition);

            serviceResponse.Response = new ResponseBaseDto()
            {
                IsSuccess = true
            };
        }
        catch (Exception)
        {
            serviceResponse.SetProperties(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado. Intentelo nuevamente.");
            return serviceResponse;
        }

        return serviceResponse;
    }

    public async Task<ResponseDto<ResponseBaseDto>> UpdateRoom(UpdateRoomRequestDto updateRoomRequestDto)
    {
        ResponseDto<ResponseBaseDto> serviceResponse = new();

        try
        {

            var hotelExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Hotel>(_hotelCollectionName, updateRoomRequestDto?.HotelId);
            if (!hotelExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El hotel con id {updateRoomRequestDto.HotelId} no existe.");
                return serviceResponse;
            }

            var alreadyExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Room>(_roomCollectionName, updateRoomRequestDto?.Id);
            if (!alreadyExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"La habitación con id {updateRoomRequestDto?.Id} no existe.");
                return serviceResponse;
            }


            var result = await _dataBaseContextRepository.UpdateDocumentByIdAsync(_roomCollectionName, updateRoomRequestDto.Id, BuildUpdateDefinition(updateRoomRequestDto));

            serviceResponse.Response = new ResponseBaseDto()
            {
                IsSuccess = true
            };
        }
        catch (Exception)
        {
            serviceResponse.SetProperties(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado. Intentelo nuevamente.");
            return serviceResponse;
        }

        return serviceResponse;
    }


    public async Task<ResponseDto<FindRoomResponseDto>> FindRoom(FindRoomRequestDto findRoomRequestDto)
    {
        ResponseDto<FindRoomResponseDto> serviceResponse = new();
        try
        {
            var rooms = await _dataBaseContextRepository.GetAllDocumentsInCollectionAsync<Room>(_roomCollectionName);
            if (rooms == null || !rooms.Any())
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"No hay habitaciones disponibles");
                return serviceResponse;
            }

            var roomsByHotelId = rooms.Where(r => r.HotelId == new ObjectId(findRoomRequestDto.HotelId) && r.IsEnabled).ToList();
            if (roomsByHotelId == null || !roomsByHotelId.Any())
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"No hay habitaciones disponibles en el hotel seleccionado");
                return serviceResponse;
            }

            //TODO: Excluir de la lista de habitaciones aquella que ya tienen reserva en la fecha del Request
            serviceResponse.Response = new FindRoomResponseDto
            {
                Rooms = roomsByHotelId.Select(r => r.TMapper<FindRoom>()).ToList()
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

    private UpdateDefinition<Room> BuildUpdateDefinition(UpdateRoomRequestDto updateRoomRequestDto)
    {
        List<UpdateDefinition<Room>> updateDefinitions = new();

        if (!string.IsNullOrEmpty(updateRoomRequestDto.RoomNumber))
            updateDefinitions.Add(Builders<Room>.Update.Set(room => room.RoomNumber, updateRoomRequestDto.RoomNumber));

        if (updateRoomRequestDto.BaseCost != null && updateRoomRequestDto.BaseCost >= 0)
            updateDefinitions.Add(Builders<Room>.Update.Set(room => room.BaseCost, updateRoomRequestDto.BaseCost));

        if (updateRoomRequestDto.Tax != null && updateRoomRequestDto.Tax >= 0)
            updateDefinitions.Add(Builders<Room>.Update.Set(room => room.Tax, updateRoomRequestDto.Tax));

        if (Enum.IsDefined(typeof(RoomTypes), updateRoomRequestDto.RoomType))
            updateDefinitions.Add(Builders<Room>.Update.Set(room => room.RoomType, updateRoomRequestDto.RoomType));

        UpdateDefinition<Room> updateDefinition = Builders<Room>.Update.Combine(updateDefinitions);
        return updateDefinition;
    }
    #endregion private methods
}
