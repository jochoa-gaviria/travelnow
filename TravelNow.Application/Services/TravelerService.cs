
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models.Room;
using TravelNow.Business.Models.Traveler;
using TravelNow.Common.Enums;
using TravelNow.Common.Helpers;
using TravelNow.Common.Interfaces;
using TravelNow.Common.Models;
using TravelNow.DataAccess.Contracts.Entities;
using TravelNow.DataAccess.Contracts.Interfaces;

namespace TravelNow.Application.Services;

public class TravelerService : ITravelerService
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
    private readonly string _passengerCollectionName;

    /// <summary>
    /// Nombre de la colección de hotel de base de datos
    /// </summary>
    private readonly string _emergencyPassengerCollectionName;
    #endregion internals

    #region consutructor

    public TravelerService(IDataBaseContextRepository dataBaseContextRepository, ICollectionNameHelper collectionNameHelper)
    {
        _dataBaseContextRepository = dataBaseContextRepository;
        _collectionNameHelper = collectionNameHelper;
        _passengerCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.passerger];
        _emergencyPassengerCollectionName = _collectionNameHelper.CollectionNames[ECollectionName.emergencyPasseger];
    }

    #endregion constructor

    #region methods

    public async Task<ResponseDto<TravelerResponseBaseDto>> CreatePassenger(CreatePassengerRequestDto createPassengerRequestDto)
    {
        ResponseDto<TravelerResponseBaseDto> serviceResponse = new();
        try
        {
            FilterDefinition<Passenger> filter = new BsonDocument(nameof(createPassengerRequestDto.IdentificationNumber), createPassengerRequestDto.IdentificationNumber);
            var passengerAlreadyExists = await _dataBaseContextRepository.ExistsDocumentAsync(_passengerCollectionName, filter);
            if (passengerAlreadyExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El pasajero con identificación {createPassengerRequestDto.IdentificationNumber} ya existe.");
                return serviceResponse;
            }

            if (!Enum.IsDefined(typeof(EGender), createPassengerRequestDto.Gender))
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El genero {createPassengerRequestDto.Gender} no es válido");
                return serviceResponse;
            }

            if (!Enum.IsDefined(typeof(EIdentificationType), createPassengerRequestDto.IdentificationType))
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El tipo de identificación {createPassengerRequestDto.IdentificationType} no es válido");
                return serviceResponse;
            }

            var result = await _dataBaseContextRepository.InsertDocumentAsync(_passengerCollectionName, createPassengerRequestDto.TMapper<Passenger>());

            serviceResponse.Response = new TravelerResponseBaseDto
            {
                Id = result.Id.ToString(),
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

    public async Task<ResponseDto<TravelerResponseBaseDto>> CreatePassengerEmergencyContact(CreatePassengerEmergencyContactRequestDto createPassengerEmergencyContactRequestDto)
    {
        ResponseDto<TravelerResponseBaseDto> serviceResponse = new();
        try
        {
            var passengerAlreadyExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Passenger>(_passengerCollectionName, createPassengerEmergencyContactRequestDto.PassengerId);
            if (!passengerAlreadyExists)
            {
                serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El pasajero con id {createPassengerEmergencyContactRequestDto.PassengerId} no existe.");
                return serviceResponse;
            }

            var result = await _dataBaseContextRepository.InsertDocumentAsync(_emergencyPassengerCollectionName, createPassengerEmergencyContactRequestDto.TMapper<EmergencyPassenger>());

            serviceResponse.Response = new TravelerResponseBaseDto
            {
                Id = result.Id.ToString(),
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

    #endregion methods

}
