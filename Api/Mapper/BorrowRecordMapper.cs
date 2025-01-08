using WebApplication1.Dto.BorrowRecordDto;
using WebApplication1.Model;

namespace WebApplication1.Mapper;

public static class BorrowRecordMapper
{
    public static ReturnBorrowDto ToReturnBorrowDto(this BorrowRecord model)
    {
        return new ReturnBorrowDto
        {
            Id = model.Id,
            UserId = model.UserId,
            BookId = model.BookId,
            BorrowDate = model.BorrowDate,
            ReturnDate = model.ReturnDate,
            isReturned = model.IsReturned
        };
    }
}