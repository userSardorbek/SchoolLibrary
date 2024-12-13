using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dto.BookDto;
using WebApplication1.Interfaces;
using WebApplication1.Migrations;
using WebApplication1.Model;

namespace WebApplication1.Controller;

[Route("api/book")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;

    public BookController(IBookRepository iBookRepository, IGenreRepository iGenreRepository)
    {
        _bookRepository = iBookRepository;
        _genreRepository = iGenreRepository;
    }


    [HttpGet("bookId:Guid")]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> GetBookByBookId(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book != null) return Ok(new ApiResponse<BookDto>(book));
        return NotFound(new ApiResponse<BookDto>($"Book not found {bookId}"));
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> CreateBook([FromBody] BookCreateDto bookCreateDto)
    {
        var bookDto = await _bookRepository.CreateBook(bookCreateDto);
        if (bookDto == null)
            return BadRequest(new ApiResponse<BookDto>("New book not added"));
        if (bookDto.Genres.Count == 0)
            return CreatedAtAction(nameof(GetBookByBookId), bookCreateDto,
                new ApiResponse<BookDto>(bookDto) { Error = "Such genres not found, edit your books genres please" });
        return CreatedAtAction(nameof(GetBookByBookId), bookCreateDto, new ApiResponse<BookDto>(bookDto));
    }

    [HttpPut]
    [Route("{bookId:Guid}")]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> EditBook([FromRoute] Guid bookId, [FromBody] BookEditDto bookEditDto)
    {
        var editedBook = await _bookRepository.EditBook(bookId, bookEditDto);
        if (editedBook == null)
            return BadRequest(new ApiResponse<BookDto>($"Book not found {bookId}"));
        return Ok(new ApiResponse<BookDto>(editedBook));
    }

    [HttpDelete]
    [Route("{bookId:Guid}")]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        var deleteBook = await _bookRepository.DeleteBook(bookId);
        if (deleteBook == null)
            return BadRequest(new ApiResponse<BookDto>($"Book not found {bookId}"));
        return Accepted(new ApiResponse<BookDto>(deleteBook));
    }
}