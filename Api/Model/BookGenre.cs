namespace WebApplication1.Model;

public class BookGenre
{
    public Guid BookId { get; set; } = Guid.NewGuid();
    public int GenreId { get; set; }
    public Book Book { get; set; } = null!;
    public Genre Genre { get; set; } = null!;
}