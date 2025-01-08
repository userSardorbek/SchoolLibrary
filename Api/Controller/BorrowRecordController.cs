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
[Authorize(Roles = "Admin")]
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
    
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBorrowDto>))]
    public async Task<IActionResult> GetBorrowById(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Your request model is invalid"));
        var record = await _borrowRecordRepository.GetBorrowRecordById(id);
        if (record == null)
            return NotFound(new ApiResponse<ReturnBorrowDto>("Borrow not found"));
        return Ok(new ApiResponse<ReturnBorrowDto>(record.ToReturnBorrowDto()));
    }

    [HttpPost]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBorrowDto>))]
    public async Task<IActionResult> CreateRecord([FromBody] CreateBorrowRecordDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Your request model is invalid"));
        var user = await _userRepository.GetUserByUsername(createDto.Username);
        if (user == null)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("User not found"));

        var book = await _bookRepository.GetByIdAsync(createDto.BookId);
        if (book == null)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Book not found"));
        if (book.Availability == false)
                return BadRequest(new ApiResponse<ReturnBorrowDto>("Book not available"));

        var borrowModel = await _borrowRecordRepository.AddBorrowRecord(new BorrowRecord
            { BookId = createDto.BookId, UserId = user.Id });
        if (borrowModel == null)
            return StatusCode(500, new ApiResponse<ReturnBorrowDto>("Could not created"));
        return Ok(new ApiResponse<ReturnBorrowDto>(borrowModel.ToReturnBorrowDto()));
    }

    [HttpPut]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnBorrowDto>))]
    public async Task<IActionResult> EditRecord(Guid id, EditBorrowRecordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Your request model is invalid"));
        var user = await _userRepository.GetUserByUsername(dto.Username);
        if (user == null)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("User not found"));
            
        var book = await _bookRepository.GetByIdAsync(dto.BookId);
        if (book == null)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Book not found"));
        
        if (!book.Availability)
            return BadRequest(new ApiResponse<ReturnBorrowDto>($"{dto.BookId} the book is not available"));
        
        var editBorrowRecord = await _borrowRecordRepository.EditBorrowRecord(id, dto, user, book);
        if (editBorrowRecord == null)
            return BadRequest(new ApiResponse<ReturnBorrowDto>("Borrow record did not edited"));
        return Ok(new ApiResponse<ReturnBorrowDto>(editBorrowRecord.ToReturnBorrowDto()));
    }
}