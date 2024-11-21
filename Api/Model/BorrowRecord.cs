namespace WebApplication1.Model;

public class BorrowRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid BookId { get; set; }
    public Book? Book { get; set; }

    public DateTime BorrowDate { get; set; } = DateTime.Now;

    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(14);
}