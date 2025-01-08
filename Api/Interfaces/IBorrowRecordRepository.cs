using WebApplication1.Dto.BookDto;
using WebApplication1.Dto.BorrowRecordDto;
using WebApplication1.Model;

namespace WebApplication1.Service;

public interface IBorrowRecordRepository
{
    Task<BorrowRecord?> AddBorrowRecord(BorrowRecord borrowRecord);
    Task<BorrowRecord?> GetBorrowRecordById(Guid id);
    Task<bool> ExistBorrowRecordById(Guid id);
    Task<BorrowRecord?> EditBorrowRecord(Guid id, EditBorrowRecordDto dto, User user, ReturnBookDto book);
}