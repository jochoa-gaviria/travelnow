using TravelNow.Common.Models;

namespace TravelNow.Application.Contracts.Interfaces;

public interface IHotelService
{
    /// <summary>
    /// Permite crear un hotel
    /// </summary>
    /// <returns></returns>
    Task<ResponseDto<bool>> CreateHotel();
    
    /// <summary>
    /// Permite actualizar un hotel
    /// </summary>
    /// <returns></returns>
    Task<ResponseDto<bool>> UpdateHotel();
    
    /// <summary>
    /// Permite habilitar o deshabilitar un hotel
    /// </summary>
    /// <returns></returns>
    Task<ResponseDto<bool>> DisableHotel();
}