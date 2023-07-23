
using TravelNow.Business.Models;
using TravelNow.Common.Models;
using TravelNow.Business.Models.Room;

namespace TravelNow.Application.Contracts.Interfaces;

public interface IRoomService
{
    /// <summary>
    /// Permite crear un hotel
    /// </summary>
    /// <param name="createRoomRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<CreateRoomResponseDto>> CreateRoom(CreateRoomRequestDto createRoomRequestDto);

    /// <summary>
    /// Permite actualizar un hotel
    /// </summary>
    /// <param name="updateRoomRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<ResponseBaseDto>> UpdateRoom(UpdateRoomRequestDto updateRoomRequestDto);

    /// <summary>
    /// Permite habilitar o deshabilitar un hotel
    /// </summary>
    /// <param name="disableRoomRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<ResponseBaseDto>> DisableRoom(DisableRoomRequestDto disableRoomRequestDto);

    /// <summary>
    /// Permite buscar habitaciones disponibles
    /// </summary>
    /// <param name="findRoomRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<FindRoomResponseDto>> FindRoom(FindRoomRequestDto findRoomRequestDto);
}
