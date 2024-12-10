using WebApplication1.Dto.GenreDto;
using WebApplication1.Model;

namespace WebApplication1.Mapper;

public static class GenreMapper
{
    public static GenreDto GenreToGenreDto(this Genre genre)
    {
        return new GenreDto()
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
}