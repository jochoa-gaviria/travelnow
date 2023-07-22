using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models;
using TravelNow.Business.Models.Hotel;
using TravelNow.Business.Models.HotelRequest;
using TravelNow.Common.Models;

namespace TravelNow.Application.Services
{
    public class HotelService : IHotelService
    {

        #region internals

        #endregion internals

        #region constructor

        #endregion constructor

        #region public methods
        /// <summary>
        /// Permite crear un hotel
        /// </summary>
        /// <param name="createHotelRequestDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto<CreateHotelResponseDto>> CreateHotel(CreateHotelRequestDto createHotelRequestDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permite actualizar un hotel
        /// </summary>
        /// <param name="updateHotelRequestDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto<ResponseBaseDto>> UpdateHotel(UpdateHotelRequestDto updateHotelRequestDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permite habilitar o deshabilitar un hotel
        /// </summary>
        /// <param name="disableHotelRequestDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto<ResponseBaseDto>> DisableHotel(DisableHotelRequestDto disableHotelRequestDto)
        {
            throw new NotImplementedException();
        }
        #endregion public methods
    }
}