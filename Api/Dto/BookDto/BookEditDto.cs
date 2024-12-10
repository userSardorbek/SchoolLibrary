namespace WebApplication1.Dto.BookDto;

public class BookEditDto
{
    public string Title { get; set; } 
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool Availability { get; set; } 
    public int[] GenreId { get; set; }
}