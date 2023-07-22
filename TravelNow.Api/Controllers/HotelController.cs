using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models;
using TravelNow.Business.Models.Hotel;
using TravelNow.Business.Models.HotelRequest;
using TravelNow.Common.Extensions;
using TravelNow.Common.Models;

namespace TravelNow.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
public class HotelController : ControllerBase
{
    #region internals
    private readonly IHotelService _hotelService;
    #endregion internals

    #region constructor
    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    #endregion constructor


    #region API's

    [HttpPost]
    [ProducesResponseType(typeof(CreateHotelResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelRequestDto createHotelRequestDto)
    {
        var response = await _hotelService.CreateHotel(createHotelRequestDto);
        return Result.ExecuteResult(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateHotel([FromBody] UpdateHotelRequestDto updateHotelRequestDto)
    {
        var response = await _hotelService.UpdateHotel(updateHotelRequestDto);
        return Result.ExecuteResult(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DisableHotel([FromBody] DisableHotelRequestDto disableHotelRequestDto) 
    {
        var response = await _hotelService.DisableHotel(disableHotelRequestDto);
        return Result.ExecuteResult(response);
    }

    #endregion API's
}