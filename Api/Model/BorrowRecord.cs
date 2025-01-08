using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("borrowRecord")]
public class BorrowRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string UserId { get; set; }
    public User? User { get; set; }
    
    public Guid BookId { get; set; }
    public Book? Book { get; set; }

    public DateTime BorrowDate { get; set; } = DateTime.UtcNow;

    public DateTime ReturnDate { get; set; } = DateTime.UtcNow.AddDays(14);

    public bool IsReturned { get; set; } = false;
}