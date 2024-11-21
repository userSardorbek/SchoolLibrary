using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
}