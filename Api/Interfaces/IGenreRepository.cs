using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface IGenreRepository
{
    Task<Genre?> GetGenreByName(string name);
}