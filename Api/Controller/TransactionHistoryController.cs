using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Dto.TransactionHistoryResult;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;
using WebApplication1.Model;

namespace WebApplication1.Controller;

[Route("api/transaction")]
[ApiController]
public class TransactionHistoryController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly UserManager<User> _userManager;
    private readonly IBookRepository _bookRepository;
    
    public TransactionHistoryController(ITransactionRepository transactionRepository, UserManager<User> userManager, IBookRepository bookRepository)
    {
        _userManager = userManager;
        _bookRepository = bookRepository;
        _transactionRepository = transactionRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Librarian")]
    public async Task<IActionResult> CreateTransaction(CreateTransactionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model state invalid");

        var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
        var librarian = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (librarian == null) 
            return BadRequest(new ApiResponse<ReturnTransactionDto>($"{dto.Username} librarian not found"));

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == dto.Username);
        if (user == null)
            return BadRequest(new ApiResponse<ReturnTransactionDto>($"{dto.Username} user not found"));
        
        var transactionHistory = await _transactionRepository.CreateTransaction(dto, user.Id, librarian.Id);
        if (transactionHistory == null)
            return BadRequest(new ApiResponse<ReturnUserDto>("Transaction didn't created. Maybe book not avaible"));

        return Ok(new ApiResponse<ReturnTransactionDto>(await transactionHistory.TransactionToReturnDto()));
    }
}