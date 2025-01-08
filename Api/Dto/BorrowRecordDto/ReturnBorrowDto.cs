namespace WebApplication1.Dto.BorrowRecordDto;

public class ReturnBorrowDto
{
    public Guid Id { get; set; } 
    public string UserId { get; set; }
    public Guid BookId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool isReturned { get; set; }
}