using WebApplication1.Dto.BookDto;
using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface IBookRepository
{
    Task<ReturnBookDto?> GetByIdAsync(Guid id);
    Task<ReturnBookDto?> CreateBook(BookCreateDto bookCreateDto);
    Task<ReturnBookDto?> EditBook(Guid bookId, BookEditDto bookEditDto);
    Task<ReturnBookDto?> DeleteBook(Guid bookId);
    Task<bool> Exist(Guid guid);
}