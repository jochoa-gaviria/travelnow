using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
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
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateHotel()
    {
        var response = await _hotelService.CreateHotel();
        return Result.ExecuteResult(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateHotel()
    {
        var response = await _hotelService.UpdateHotel();
        return Result.ExecuteResult(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DisableHotel() 
    {
        var response = await _hotelService.DisableHotel();
        return Result.ExecuteResult(response);
    }

    #endregion API's
}