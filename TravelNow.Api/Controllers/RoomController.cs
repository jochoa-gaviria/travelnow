using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Application.Services;
using TravelNow.Business.Models;
using TravelNow.Business.Models.Room;
using TravelNow.Common.Extensions;
using TravelNow.Common.Models;

namespace TravelNow.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
public class RoomController : ControllerBase
{
    #region internals 

    /// <summary>
    /// Servicio de habitaciones
    /// </summary>
    private readonly IRoomService _roomService;
    #endregion internals

    #region constructor
    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    #endregion constructor

    #region methods

    [HttpPost]
    [ProducesResponseType(typeof(CreateRoomResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequestDto createRoomRequestDto)
    {
        var response = await _roomService.CreateRoom(createRoomRequestDto);
        return Result.ExecuteResult(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomRequestDto updateRoomRequestDto)
    {
        var response = await _roomService.UpdateRoom(updateRoomRequestDto);
        return Result.ExecuteResult(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DisableRoom([FromBody] DisableRoomRequestDto disableRoomRequestDto)
    {
        var response = await _roomService.DisableRoom(disableRoomRequestDto);
        return Result.ExecuteResult(response);
    }

    #endregion methods
}
