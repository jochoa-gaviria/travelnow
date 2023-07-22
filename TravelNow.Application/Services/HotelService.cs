using TravelNow.Application.Contracts.Interfaces;
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
        /// <returns></returns>
        public Task<ResponseDto<bool>> CreateHotel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permite actualizar un hotel
        /// </summary>
        /// <returns></returns>
        public Task<ResponseDto<bool>> DisableHotel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permite habilitar o deshabilitar un hotel
        /// </summary>
        /// <returns></returns>
        public Task<ResponseDto<bool>> UpdateHotel()
        {
            throw new NotImplementedException();
        }
        #endregion public methods
    }
}