using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model;

[Table("users")]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(UserName), IsUnique = true)]
public class User : IdentityUser
{
    [Required] 
    public string FullName { get; set; }

    [Required] 
    public override string UserName { get; set; }

    [Required]
    public override string NormalizedUserName { get; set; }

    [Required] 
    public override string Email { get; set; }

    [Required]
    public override string NormalizedEmail { get; set; }

    [Required] 
    public override string PasswordHash { get; set; }

    public List<BorrowRecord> BorrowRecords { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public List<TransactionHistory> UserTransactionHistories { get; set; } = [];
    public List<TransactionHistory> LibrarianTransactionHistories { get; set; } = [];
}