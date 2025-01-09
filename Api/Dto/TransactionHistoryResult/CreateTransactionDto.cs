using System.ComponentModel.DataAnnotations;
using WebApplication1.Model;

namespace WebApplication1.Dto.TransactionHistoryResult;

public class CreateTransactionDto
{
    [Required]
    public ActionTransaction Action { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public Guid BookId { get; set; }
}