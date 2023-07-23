using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Business.Models.Book;
using TravelNow.Common.Extensions;
using TravelNow.Common.Models;

namespace TravelNow.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
public class BookingController : ControllerBase
{

    #region internals
    private readonly IBookingService _bookingService;
    #endregion internals

    #region constructor
    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    #endregion constructor

    #region methods

    [HttpGet]
    [ProducesResponseType(typeof(BookListResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListBook()
    {
        var response = await _bookingService.ListBook();
        return Result.ExecuteResult(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(BookDetailResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> BookDetail([FromQuery] string id)
    {
        var response = await _bookingService.BookDetail(id);
        return Result.ExecuteResult(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Book([FromBody] BookRequestDto bookRequestDto)
    {
        var response = await _bookingService.Book(bookRequestDto);
        return Result.ExecuteResult(response);
    }

    #endregion methods
}
