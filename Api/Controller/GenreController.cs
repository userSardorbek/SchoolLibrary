using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dto.GenreDto;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;
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

    // [HttpGet]
    // [Route("{name}")]
    // [ProducesDefaultResponseType(typeof(ApiResponse<Genre>))]
    // public async Task<IActionResult> GetGenreByName(string name)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState); //Idk what is that
    //     var genre = await _genreRepository.GetByName(name);
    //     if (genre == null)
    //         return NotFound(new ApiResponse<Genre>("Book not found"));
    //     return Ok(new ApiResponse<Genre>(genre));
    // }

    [HttpGet]
    [Authorize]
    [Route("id:int")]
    [ProducesDefaultResponseType(typeof(ApiResponse<GenreDto>))]
    public async Task<IActionResult> GetById(int id)
    {
        var genre = await _genreRepository.GetById(id);
        if (genre == null)
            return NotFound(new ApiResponse<Genre>("Such genre not found"));
        return Ok(new ApiResponse<GenreDto>(genre));
    }

    [HttpGet]
    [ProducesDefaultResponseType(typeof(ApiResponse<List<GenreDto>>))]
    public async Task<IActionResult> GetAllGenres()
    {
        var allGenres = await _genreRepository.GetAllGenres();
        return Ok(new ApiResponse<List<GenreDto>>(allGenres));
    }

    [HttpPost]
    [Route("{name}")]
    [ProducesDefaultResponseType(typeof(ApiResponse<GenreDto>))]
    public async Task<IActionResult> CreateGenre([FromRoute] string name)
    {
        var byName = await _genreRepository.GetByName(name);
        if (byName != null)
            return BadRequest(new ApiResponse<string>($"{name} is already exist"));
        var genre = await _genreRepository.CreateGenre(new Genre() { Name = name });
        return CreatedAtAction(nameof(GetById), new { id = genre.Id },
            new ApiResponse<GenreDto>(genre.GenreToGenreDto()));
    }

    [HttpPut]
    [ProducesDefaultResponseType(typeof(ApiResponse<GenreDto>))]
    public async Task<IActionResult> UpdateGenre([FromBody] GenreDto genreDto)
    {
        var byName = await _genreRepository.GetByName(genreDto.Name);
        if (byName != null)
            return BadRequest(new ApiResponse<GenreDto>($"{genreDto.Name} is already exist"));
        
        var updateGenre = await _genreRepository.UpdateGenre(genreDto);
        if (updateGenre == null)
            return BadRequest(new ApiResponse<GenreDto>($"{genreDto.Id} Such genre not found"));
        return Ok(new ApiResponse<GenreDto>(updateGenre));
    }

    [HttpDelete]
    [Route("id:int")]
    [ProducesDefaultResponseType(typeof(ApiResponse<GenreDto>))]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var deleteGenre = await _genreRepository.DeleteGenre(id);
        if (deleteGenre == null)
            return BadRequest(new ApiResponse<GenreDto>($"{id} Such genre not found"));
        return Ok(new ApiResponse<GenreDto>(deleteGenre));
    } 
}