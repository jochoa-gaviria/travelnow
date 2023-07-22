using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNow.Common.Models;

namespace TravelNow.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
public class BookingController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public Task<IActionResult> ListBook()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public Task<IActionResult> BookDetail()
    {
        throw new NotImplementedException();
    }
}
