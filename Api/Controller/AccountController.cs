using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;
using WebApplication1.Model;

namespace WebApplication1.Controller;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    // [ProducesDefaultResponseType(typeof(ApiResponse<>))]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User()
            {
                UserName = dto.Username,
                FullName = dto.FullName,
                Email = dto.Email
            };

            var createdUser = await _userManager.CreateAsync(user, dto.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Student");
                return roleResult.Succeeded ? Ok("User created") : StatusCode(500, roleResult.Errors);
            }

            return StatusCode(500, createdUser.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

//     [HttpGet]
//     [ProducesDefaultResponseType(typeof(ApiResponse<List<ReturnUserDto>>))]
//     public async Task<IActionResult> GetAllUsers()
//     {
//         var users = await _userRepository.GetAllUsersAsync();
//         var returnUsers = users.Select(s => s.FromUserToReturnUserDto());
//
//         return Ok(new ApiResponse<IEnumerable<ReturnUserDto>>(returnUsers));
//     }
//
//     [HttpGet("{id:guid}")]
//     [ProducesDefaultResponseType(typeof(ApiResponse<ReturnUserDto>))]
//     public async Task<IActionResult> GetUserByUserId([FromRoute] Guid id)
//     {
//         var user = await _userRepository.GetUserByIdAsync(id);
//         if (user != null) return Ok(new ApiResponse<ReturnUserDto>(user.FromUserToReturnUserDto()));
//
//         return NotFound(new ApiResponse<ReturnUserDto>("User not found"));
//     }
//
//     [HttpPost]
//     [ProducesDefaultResponseType(typeof(ApiResponse<ReturnUserDto>))]
//     public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
//     {
//         if (createUserDto.Username.Length > 70 || createUserDto.Username.Length < 6)
//             return BadRequest(new ApiResponse<ReturnUserDto>("Username must be between 6 and 70"));
//         
//         if (createUserDto.Password.Length is >= 101 or <= 8)
//             return BadRequest(new ApiResponse<ReturnUserDto>("Password must be between 8 and 101"));
//         
//         var user = await _userRepository.CreateAsync(createUserDto);
//         return CreatedAtAction(nameof(GetUserByUserId), new { id = user.UserId }, new ApiResponse<ReturnUserDto>(user.FromUserToReturnUserDto()));
//     }
//
//     [HttpPut]
//     [ProducesDefaultResponseType(typeof(ApiResponse<ReturnUserDto>))]
//     public async Task<IActionResult> UpdateUser([FromRoute] Guid id)
//     {
//         return NotFound(new ApiResponse<ReturnUserDto>("User not found"));
//     }
}