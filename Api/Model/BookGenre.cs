using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("bookGenre")]
public class BookGenre
{
    public Guid BookId { get; set; }
    public int GenreId { get; set; }
    public Book Book { get; set; } = null!;
    public Genre Genre { get; set; } = null!;
}