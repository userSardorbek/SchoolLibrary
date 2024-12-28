using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Dto.BorrowRecordDto;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;
using WebApplication1.Model;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[Route("/api/borrowRecords")]
[ApiController]
public class BorrowRecordController : ControllerBase
{
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    // private readonly UserManager<User> _userManager;

    public BorrowRecordController(IBorrowRecordRepository borrowRepo, IBookRepository bookRepository,
        IUserRepository userRepository)
    {
        _borrowRecordRepository = borrowRepo;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        // _userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBorrowDto>))]
    public async Task<IActionResult> CreateRecord([FromBody] CreateBorrowRecordDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Your request model is not valid"));
        var user = await _userRepository.GetUserByUsername(createDto.Username);
        if (user == null)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("User not found"));

        var book = await _bookRepository.GetByIdAsync(createDto.BookId);
        if (book == null)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Book not found"));

        var borrowModel = await _borrowRecordRepository.AddBorrowRecord(new BorrowRecord
            { BookId = createDto.BookId, UserId = user.Id });
        if (borrowModel == null)
            return StatusCode(500, new ApiResponse<ReturnBorrowDto>("Could not created"));
        // return CreatedAtAction()
        return Ok(new ApiResponse<ReturnBorrowDto>(borrowModel.ToReturnBorrowDto()));
    }
}