using WebApplication1.Model;

namespace WebApplication1.Dto.BookDto;

public class ReturnBookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool Availability { get; set; }
    public List<GenreDto.GenreDto> Genres { get; set; }
}