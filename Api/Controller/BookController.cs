using Microsoft.AspNetCore.Authorization;
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
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBookDto>))]
    public async Task<IActionResult> GetBookByBookId(Guid bookId)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBookDto>("There is an error in your request form."));
        var book = await iBookRepository.GetByIdAsync(bookId);
        if (book != null) return Ok(new ApiResponse<ReturnBookDto>(book));
        return NotFound(new ApiResponse<ReturnBookDto>($"Book not found {bookId}"));
    }

    [HttpPost]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBookDto>))]
    public async Task<IActionResult> CreateBook([FromBody] BookCreateDto bookCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBookDto>("There is an error in your request form."));
        var bookDto = await iBookRepository.CreateBook(bookCreateDto);
        if (bookDto == null)
            return BadRequest(new ApiResponse<ReturnBookDto>("New book not added"));
        if (bookDto.Genres.Count == 0)
            return CreatedAtAction(nameof(GetBookByBookId), bookCreateDto,
                new ApiResponse<ReturnBookDto>(bookDto) { Error = "Such genres not found, edit your book's genres please" });
        return CreatedAtAction(nameof(GetBookByBookId), bookCreateDto, new ApiResponse<ReturnBookDto>(bookDto));
    }

    [HttpPut]
    [Route("{bookId:Guid}")]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBookDto>))]
    public async Task<IActionResult> EditBook([FromRoute] Guid bookId, [FromBody] BookEditDto bookEditDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBookDto>("There is an error in your request form."));
        var editedBook = await iBookRepository.EditBook(bookId, bookEditDto);
        if (editedBook == null)
            return BadRequest(new ApiResponse<ReturnBookDto>($"Book not found {bookId}"));
        return Ok(new ApiResponse<ReturnBookDto>(editedBook));
    }

    [HttpDelete]
    [Route("{bookId:Guid}")]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBookDto>))]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBookDto>("There is an error in your request form."));
        var deleteBook = await iBookRepository.DeleteBook(bookId);
        if (deleteBook == null)
            return BadRequest(new ApiResponse<ReturnBookDto>($"Book not found {bookId}"));
        return Accepted(new ApiResponse<ReturnBookDto>(deleteBook));
    }
}