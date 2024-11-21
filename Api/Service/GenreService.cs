using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Service;

public class GenreService: IGenreRepository
{
    private readonly ApplicationDbContext _context;
    
    // should be tested at first
    public async Task<Genre?> GetGenreByName(string name)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Name == name);
        return genre ?? null; // it returns null if genre == null
    }
}