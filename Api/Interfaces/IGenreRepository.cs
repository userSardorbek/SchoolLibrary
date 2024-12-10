using WebApplication1.Dto.GenreDto;
using WebApplication1.Model;

namespace WebApplication1.Interfaces;

public interface IGenreRepository
{
    Task<GenreDto?> GetById(int id);
    Task<List<GenreDto>> GetAllGenres();
    Task<Genre?> GetByName(string name);
    Task<Genre> CreateGenre(Genre genreModel);
    Task<GenreDto?> UpdateGenre(GenreDto genreDto);
    Task<GenreDto?> DeleteGenre(int id);

}