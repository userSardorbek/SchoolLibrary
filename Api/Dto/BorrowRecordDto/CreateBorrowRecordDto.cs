using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto.BorrowRecordDto;

public class CreateBorrowRecordDto
{
    [Required] 
    public string Username { get; set; }
    [Required]
    public Guid BookId { get; set; }
}