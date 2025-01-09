using WebApplication1.Dto.TransactionHistoryResult;
using WebApplication1.Model;

namespace WebApplication1.Mapper;

public static class TransactionMapper
{
    public static async Task<ReturnTransactionDto> TransactionToReturnDto(this TransactionHistory model)
    {
        return new ReturnTransactionDto
        {
            Action = model.Action, 
            ActionDate = model.ActionDate, 
            BookId = model.BookId, 
            UserId = model.UserId,
            LibrarianId = model.LibrarianId
        };
    }
}