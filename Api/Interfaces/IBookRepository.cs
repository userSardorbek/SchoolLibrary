using WebApplication1.Dto.BookDto;
using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface IBookRepository
{
    Task<BookDto?> GetByIdAsync(Guid id);
    Task<BookDto?> CreateBook(BookCreateDto bookCreateDto);
    Task<BookDto?> EditBook(Guid bookId, BookEditDto bookEditDto);
    Task<BookDto?> DeleteBook(Guid bookId);
}