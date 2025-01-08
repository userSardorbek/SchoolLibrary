using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto.BookDto;
using WebApplication1.Dto.BorrowRecordDto;
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
        var book = await _context.Books.FindAsync(borrowRecord.BookId);
        if (book != null) book.Availability = false;
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<BorrowRecord?> GetBorrowRecordById(Guid id)
    {
        return await _context.BorrowRecords.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistBorrowRecordById(Guid id)
    {
        return await _context.BorrowRecords.AnyAsync(x => x.Id == id);
    }

    public async Task<BorrowRecord?> EditBorrowRecord(Guid id, EditBorrowRecordDto dto, User user, ReturnBookDto book)
    {
        var borrowRecord = await _context.BorrowRecords.FindAsync(id);
        if (borrowRecord == null)
            return null;
        borrowRecord.UserId = user.Id;
        borrowRecord.BookId = book.Id;
        borrowRecord.ReturnDate = dto.ReturnDate;
        borrowRecord.IsReturned = dto.isReturned;
        var editBook = await _context.Books.FindAsync(book.Id);
        if (editBook != null) editBook.Availability = false;
        await _context.SaveChangesAsync();
        return borrowRecord;
    }
}