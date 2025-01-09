using WebApplication1.Dto.TransactionHistoryResult;
using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface ITransactionRepository
{
    Task<TransactionHistory?> CreateTransaction(CreateTransactionDto dto, string librarianId, string userId);
}