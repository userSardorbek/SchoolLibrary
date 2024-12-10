using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("transactionHistory")]
public class TransactionHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ActionTransaction Action { get; set; }
    public DateTime ActionDate { get; set; } = DateTime.Now;
    
    public List<Comment> Comments { get; set; } = [];

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? LibrarianId { get; set; }
    public User? Librarian { get; set; }

    public Guid? BookId { get; set; }
    public Book? Book { get; set; }
    
}