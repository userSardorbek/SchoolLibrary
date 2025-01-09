using WebApplication1.Data;
using WebApplication1.Dto.TransactionHistoryResult;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<TransactionHistory?> CreateTransaction(CreateTransactionDto dto, string librarianId, string userId)
    {
        var book = await _context.Books.FindAsync(dto.BookId);
        if (book == null || book.Availability == false)
            return null;
        
        var transaction = new TransactionHistory
        {
            BookId = dto.BookId,
            LibrarianId = librarianId,
            UserId = userId,
            Action = dto.Action
        };
        book.Availability = false;
        var result = await _context.SaveChangesAsync();
        if (result == 0)
            return null;
        return transaction;
    }
}