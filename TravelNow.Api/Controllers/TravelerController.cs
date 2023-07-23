using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models.Traveler;
using TravelNow.Common.Extensions;
using TravelNow.Common.Models;

namespace TravelNow.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
public class TravelerController : ControllerBase
{
    #region internals
    private readonly ITravelerService _travelerService;
    #endregion internals

    #region constructor
    public TravelerController(ITravelerService travelerService)
    {
        _travelerService = travelerService;
    }
    #endregion constructor

    #region methods
    [HttpPost]
    [ProducesResponseType(typeof(TravelerResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreatePassenger([FromBody] CreatePassengerRequestDto createPassengerRequestDto)
    {
        var response = await _travelerService.CreatePassenger(createPassengerRequestDto);
        return Result.ExecuteResult(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TravelerResponseBaseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreatePassengerEmergencyContact([FromBody] CreatePassengerEmergencyContactRequestDto createPassengerEmergencyContactRequestDto)
    {
        var response = await _travelerService.CreatePassengerEmergencyContact(createPassengerEmergencyContactRequestDto);
        return Result.ExecuteResult(response);
    }
    #endregion methods
}
