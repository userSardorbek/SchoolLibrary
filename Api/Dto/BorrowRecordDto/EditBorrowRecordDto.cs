using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto.BorrowRecordDto;

public class EditBorrowRecordDto
{
    [Required] 
    public string Username { get; set; }
    [Required]
    public Guid BookId { get; set; }
    [Required]
    public DateTime ReturnDate { get; set; }
    [Required]
    public bool isReturned { get; set; }
}