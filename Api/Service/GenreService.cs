using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto.GenreDto;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;
using WebApplication1.Model;

namespace WebApplication1.Service;

public class GenreService : IGenreRepository
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenreDto?> GetById(int id)
    {
        var genreDto = await _context.Genres.Where(g => g.Id == id).Select(g => new GenreDto() { Id = g.Id, Name = g.Name }).FirstOrDefaultAsync();
        return genreDto;
    }

    public async Task<List<GenreDto>> GetAllGenres()
    {
        return await _context.Genres.Select(g => new GenreDto(){Id = g.Id, Name = g.Name}).ToListAsync();
    }

    public async Task<Genre?> GetByName(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Genre> CreateGenre(Genre genreModel)
    {
        await _context.Genres.AddAsync(genreModel);
        await _context.SaveChangesAsync();
        return genreModel;
    }

    public async Task<GenreDto?> UpdateGenre(GenreDto genreDto)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(x=> x.Id== genreDto.Id);
        if (genre == null)
            return null;
        genre.Name = genreDto.Name;
        await _context.SaveChangesAsync();
        return genre.GenreToGenreDto();
    }

    public async Task<GenreDto?> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (genre == null)
            return null;
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return genre.GenreToGenreDto();
    }
}