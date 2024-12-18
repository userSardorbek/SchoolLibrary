using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("book")]
public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool Availability { get; set; } = true;

    public List<BookGenre> BookGenres { get; set; } = [];
    public List<BorrowRecord> BorrowRecords { get; set; } = [];
    public List<TransactionHistory> TransactionHistories { get; set; } = [];
}