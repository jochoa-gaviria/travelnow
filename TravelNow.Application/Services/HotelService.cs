using MongoDB.Bson;
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

        /// <summary>
        /// Nombre de la colección de base de datos
        /// </summary>
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
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El hotel {createHotelRequestDto.Name} ya existe.");
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
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El hotel con id {updateHotelRequestDto.Id} no existe.");
                    return serviceResponse;
                }

                var result = await _dataBaseContextRepository.UpdateDocumentByIdAsync(_collectionName, updateHotelRequestDto.Id, BuildUpdateDefinitions(updateHotelRequestDto));

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
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"El hotel con id {disableHotelRequestDto.Id} no existe.");
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


        public async Task<ResponseDto<FindHotelResponseDto>> FindHotel(FindHotelRequestDto findHotelRequestDto)
        {
            ResponseDto<FindHotelResponseDto> serviceResponse = new();
            try
            {
                var hotels = await _dataBaseContextRepository.GetAllDocumentsInCollectionAsync<Hotel>(_collectionName);
                if (hotels == null || !hotels.Any())
                {
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"No hay hoteles disponibles");
                    return serviceResponse;
                }

                var hotelsByCity = hotels.Where(h => h.IsEnabled && h.City == findHotelRequestDto.City).ToList();
                if (hotelsByCity == null || !hotelsByCity.Any())
                {
                    serviceResponse.SetProperties(HttpStatusCode.BadRequest, $"No hay hoteles disponibles en la ciudad {findHotelRequestDto.City}");
                    return serviceResponse;
                }


                serviceResponse.Response = new FindHotelResponseDto()
                {
                    Hotels = hotelsByCity.Select(h => h.TMapper<FindHotel>()).ToList()
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


        #region private methods
        private UpdateDefinition<Hotel> BuildUpdateDefinitions(UpdateHotelRequestDto updateHotelRequestDto)
        {

            List<UpdateDefinition<Hotel>> updateDefinitions = new();

            if (!string.IsNullOrEmpty(updateHotelRequestDto.Name))
                updateDefinitions.Add(Builders<Hotel>.Update.Set(hotel => hotel.Name, updateHotelRequestDto.Name));

            if (!string.IsNullOrEmpty(updateHotelRequestDto.Country))
                updateDefinitions.Add(Builders<Hotel>.Update.Set(hotel => hotel.Country, updateHotelRequestDto.Country));

            if (!string.IsNullOrEmpty(updateHotelRequestDto.City))
                updateDefinitions.Add(Builders<Hotel>.Update.Set(hotel => hotel.City, updateHotelRequestDto.City));

            UpdateDefinition<Hotel> updateDefinition = Builders<Hotel>.Update.Combine(updateDefinitions);
            return updateDefinition;
        }

        #endregion private methods
    }
}