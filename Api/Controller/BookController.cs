using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dto.BookDto;
using WebApplication1.Interfaces;

namespace WebApplication1.Controller;

[Route("api/book")]
[ApiController]
public class BookController(IBookRepository iBookRepository) : ControllerBase
{
    [HttpGet("bookId:Guid")]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> GetBookByBookId(Guid bookId)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<BookDto>("There is an error in your request form."));
        var book = await iBookRepository.GetByIdAsync(bookId);
        if (book != null) return Ok(new ApiResponse<BookDto>(book));
        return NotFound(new ApiResponse<BookDto>($"Book not found {bookId}"));
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> CreateBook([FromBody] BookCreateDto bookCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<BookDto>("There is an error in your request form."));
        var bookDto = await iBookRepository.CreateBook(bookCreateDto);
        if (bookDto == null)
            return BadRequest(new ApiResponse<BookDto>("New book not added"));
        if (bookDto.Genres.Count == 0)
            return CreatedAtAction(nameof(GetBookByBookId), bookCreateDto,
                new ApiResponse<BookDto>(bookDto) { Error = "Such genres not found, edit your book's genres please" });
        return CreatedAtAction(nameof(GetBookByBookId), bookCreateDto, new ApiResponse<BookDto>(bookDto));
    }

    [HttpPut]
    [Route("{bookId:Guid}")]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> EditBook([FromRoute] Guid bookId, [FromBody] BookEditDto bookEditDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<BookDto>("There is an error in your request form."));
        var editedBook = await iBookRepository.EditBook(bookId, bookEditDto);
        if (editedBook == null)
            return BadRequest(new ApiResponse<BookDto>($"Book not found {bookId}"));
        return Ok(new ApiResponse<BookDto>(editedBook));
    }

    [HttpDelete]
    [Route("{bookId:Guid}")]
    [ProducesDefaultResponseType(typeof(ApiResponse<BookDto>))]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<BookDto>("There is an error in your request form."));
        var deleteBook = await iBookRepository.DeleteBook(bookId);
        if (deleteBook == null)
            return BadRequest(new ApiResponse<BookDto>($"Book not found {bookId}"));
        return Accepted(new ApiResponse<BookDto>(deleteBook));
    }
}