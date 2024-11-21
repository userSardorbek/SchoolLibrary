using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("book")]
public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    [Length(minimumLength: 13, maximumLength: 14)]
    public string ISBN { get; set; }

    [Required] 
    public bool Availability { get; set; }

    public List<BookGenre> BookGenres { get; set; } = [];
    public List<BorrowRecord> BorrowRecords { get; set; } = [];
    public List<ActionTransaction> TransactionActions { get; set; } = [];
}