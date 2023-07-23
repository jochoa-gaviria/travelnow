namespace TravelNow.Business.Models.Book;

public class BookListResponseDto
{
    /// <summary>
    /// Lista de reservas
    /// </summary>
    public List<BookRequestDto>? Books { get; set; }
}
