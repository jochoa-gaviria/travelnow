using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNow.Business.Models;
using TravelNow.Business.Models.Room;
using TravelNow.Common.Models;

namespace TravelNow.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
public class RoomController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateRoomResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public Task<IActionResult> CreateRoom([FromBody] CreateRoomRequestDto createRoomRequestDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public Task<IActionResult> UpdateRoom([FromBody] UpdateRoomRequestDto updateRoomRequestDto)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public Task<IActionResult> DisableRoom([FromBody] DisableRoomRequestDto disableRoomRequestDto)
    {
        throw new NotImplementedException();
    }
}
