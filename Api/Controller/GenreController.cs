using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Controller;

[Route("api/genre")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreRepository _genreRepository;

    public GenreController(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet()]
    [ProducesDefaultResponseType(typeof(ApiResponse<Genre>))]
    public async Task<IActionResult> GetGenreByName([FromRoute] string name)
    {
        var genre = await _genreRepository.GetGenreByName(name);
        if (genre == null)
            return NotFound(new ApiResponse<Genre>("Genre not found"));
        return Ok(new ApiResponse<Genre>(genre));
    }
}