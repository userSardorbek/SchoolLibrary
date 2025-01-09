namespace WebApplication1.Dto.TransactionHistoryResult;

public class ReturnTransactionDto
{
    public Guid Id { get; set; }
    public ActionTransaction Action { get; set; }
    public DateTime ActionDate { get; set; }
    public string UserId { get; set; }
    public string LibrarianId { get; set; }
    public Guid BookId { get; set; }
}