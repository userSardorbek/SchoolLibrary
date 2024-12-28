using WebApplication1.Model;

namespace WebApplication1.Service;

public interface IBorrowRecordRepository
{
    Task<BorrowRecord?> AddBorrowRecord(BorrowRecord borrowRecord);
}