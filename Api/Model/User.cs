using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
[Table("users")]
public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Required]
    [MinLength(6)]
    [MaxLength(70)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FullName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public List<BorrowRecord> BorrowRecords { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    // public ICollection<TransactionHistory> UserTransactions { get; set; } = new HashSet<TransactionHistory>();
    // public ICollection<TransactionHistory> LibrarianTransactions { get; set; } = new HashSet<TransactionHistory>();
    public List<TransactionHistory> UserHistories { get; set; } = [];
    public List<TransactionHistory> LibrarianHistories { get; set; } = [];

}