using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Service;

namespace WebApplication1.Repository;

public class BorrowRecordRepository : IBorrowRecordRepository
{
    private readonly ApplicationDbContext _context;

    public BorrowRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<BorrowRecord?> AddBorrowRecord(BorrowRecord borrowRecord)
    {
        var entityEntry = await _context.BorrowRecords.AddAsync(borrowRecord);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }
}