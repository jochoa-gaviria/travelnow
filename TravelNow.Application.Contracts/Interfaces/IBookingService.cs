
using TravelNow.Business.Models.Book;
using TravelNow.Common.Models;

namespace TravelNow.Application.Contracts.Interfaces;

public interface IBookingService
{
    Task<ResponseDto<BookListResponseDto>> ListBook();
    Task<ResponseDto<BookDetailResponseDto>> BookDetail(string id);
    Task<ResponseDto<BookResponseDto>> Book(BookRequestDto bookRequestDto);
}
