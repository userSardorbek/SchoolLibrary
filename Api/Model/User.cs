using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model;

[Table("users")]
// [Index(nameof(Email), IsUnique = true)]
public class User : IdentityUser
{
    public string FullName { get; set; }

    public List<BorrowRecord> BorrowRecords { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public List<TransactionHistory> UserTransactionHistories { get; set; } = [];
    public List<TransactionHistory> LibrarianTransactionHistories { get; set; } = [];

}