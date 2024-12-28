using WebApplication1.Dto.BookDto;
using WebApplication1.Dto.GenreDto;
using WebApplication1.Model;

namespace WebApplication1.Mapper;

public static class BookMapper
{
    public static ReturnBookDto BookToBookDto(this Book book, List<GenreDto> genres)
    {
        return new ReturnBookDto()
        {
            Author = book.Author,
            Availability = book.Availability,
            Genres = genres,
            Id = book.Id,
            ISBN = book.ISBN,
            Title = book.Title
        };
    }
}