namespace WebApplication1.Dto.BookDto;

public class BookCreateDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int[] GenreId { get; set; }
}