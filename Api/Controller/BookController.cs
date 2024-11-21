using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Migrations;
using WebApplication1.Model;

namespace WebApplication1.Controller;

[Route("api/book")]
public class BookController(IBookRepository iBookRepository) : ControllerBase
{
    private readonly IBookRepository _bookRepository = iBookRepository;

    [HttpGet("id::guid")]
    [ProducesDefaultResponseType(typeof(ApiResponse<Book?>))]
    public async Task<IActionResult> GetBookByBookId([FromRoute] Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book != null) return Ok(new ApiResponse<Model.Book>(book));
        return NotFound(new ApiResponse<Model.Book>("Book not found"));
    }
    
    
}