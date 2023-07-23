using TravelNow.Business.Models;
using TravelNow.Business.Models.Hotel;
using TravelNow.Business.Models.HotelRequest;
using TravelNow.Common.Models;

namespace TravelNow.Application.Contracts.Interfaces;

public interface IHotelService
{
    /// <summary>
    /// Permite crear un hotel
    /// </summary>
    /// <param name="createHotelRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<CreateHotelResponseDto>> CreateHotel(CreateHotelRequestDto createHotelRequestDto);

    /// <summary>
    /// Permite actualizar un hotel
    /// </summary>
    /// <param name="updateHotelRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<ResponseBaseDto>> UpdateHotel(UpdateHotelRequestDto updateHotelRequestDto);

    /// <summary>
    /// Permite habilitar o deshabilitar un hotel
    /// </summary>
    /// <param name="disableHotelRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<ResponseBaseDto>> DisableHotel(DisableHotelRequestDto disableHotelRequestDto);

    /// <summary>
    /// Permite buscar hoteles disponibles
    /// </summary>
    /// <param name="findHotelRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<FindHotelResponseDto>> FindHotel(FindHotelRequestDto findHotelRequestDto);
}