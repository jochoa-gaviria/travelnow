﻿using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models;
using TravelNow.Business.Models.Hotel;
using TravelNow.Business.Models.HotelRequest;
using TravelNow.Common.Enums;
using TravelNow.Common.Helpers;
using TravelNow.Common.Interfaces;
using TravelNow.Common.Models;
using TravelNow.DataAccess.Contracts.Entities;
using TravelNow.DataAccess.Contracts.Interfaces;

namespace TravelNow.Application.Services
{
    public class HotelService : IHotelService
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

        private readonly string _collectionName;
        #endregion internals

        #region constructor
        public HotelService(IDataBaseContextRepository dataBaseContextRepository, ICollectionNameHelper collectionNameHelper)
        {
            _dataBaseContextRepository = dataBaseContextRepository;
            _collectionNameHelper = collectionNameHelper;
            _collectionName = _collectionNameHelper.CollectionNames[ECollectionName.hotel];
        }

        #endregion constructor

        #region public methods
        /// <summary>
        /// Permite crear un hotel
        /// </summary>
        /// <param name="createHotelRequestDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto<CreateHotelResponseDto>> CreateHotel(CreateHotelRequestDto createHotelRequestDto)
        {
            ResponseDto<CreateHotelResponseDto> serviceResponse = new();

            try
            {
                FilterDefinition<Hotel> filter = new BsonDocument(nameof(createHotelRequestDto.Name), createHotelRequestDto.Name);
                var alreadyExists = await _dataBaseContextRepository.ExistsDocumentAsync(_collectionName, filter);
                if (alreadyExists)
                {
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, "El hotel ya existe.");
                    return serviceResponse;
                }
                
                var newHotel = createHotelRequestDto.TMapper<Hotel>();
                newHotel.IsEnabled = true;

                var result = await _dataBaseContextRepository.InsertDocumentAsync(_collectionName, newHotel);

                serviceResponse.Response = new CreateHotelResponseDto
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

        /// <summary>
        /// Permite actualizar un hotel
        /// </summary>
        /// <param name="updateHotelRequestDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto<ResponseBaseDto>> UpdateHotel(UpdateHotelRequestDto updateHotelRequestDto)
        {
            ResponseDto<ResponseBaseDto> serviceResponse = new();
            try
            {
                var alreadyExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Hotel>(_collectionName, updateHotelRequestDto?.Id);
                if (!alreadyExists)
                {
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, "El hotel no existe.");
                    return serviceResponse;
                }

                List<UpdateDefinition<Hotel>> updateDefinitions = new();

                if (!string.IsNullOrEmpty(updateHotelRequestDto.Name))
                    updateDefinitions.Add(Builders<Hotel>.Update.Set(hotel => hotel.Name, updateHotelRequestDto.Name));

                if (!string.IsNullOrEmpty(updateHotelRequestDto.Country))
                    updateDefinitions.Add(Builders<Hotel>.Update.Set(hotel => hotel.Country, updateHotelRequestDto.Country));
                
                if (!string.IsNullOrEmpty(updateHotelRequestDto.City))
                    updateDefinitions.Add(Builders<Hotel>.Update.Set(hotel => hotel.City, updateHotelRequestDto.City));

                UpdateDefinition<Hotel> updateDefinition = Builders<Hotel>.Update.Combine(updateDefinitions);

                var result = await _dataBaseContextRepository.UpdateDocumentByIdAsync(_collectionName, updateHotelRequestDto.Id, updateDefinition);

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

        /// <summary>
        /// Permite habilitar o deshabilitar un hotel
        /// </summary>
        /// <param name="disableHotelRequestDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto<ResponseBaseDto>> DisableHotel(DisableHotelRequestDto disableHotelRequestDto)
        {
            ResponseDto<ResponseBaseDto> serviceResponse = new();
            try
            {
                var alreadyExists = await _dataBaseContextRepository.ExistsDocumentByIdAsync<Hotel>(_collectionName, disableHotelRequestDto?.Id);
                if (!alreadyExists)
                {
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, "El hotel no existe.");
                    return serviceResponse;
                }


                UpdateDefinition<Hotel> updateDefinition = Builders<Hotel>.Update.Set(hotel => hotel.IsEnabled, disableHotelRequestDto.IsEnabled);

                var result = await _dataBaseContextRepository.UpdateDocumentByIdAsync(_collectionName, disableHotelRequestDto.Id, updateDefinition);

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
        #endregion public methods
    }
}