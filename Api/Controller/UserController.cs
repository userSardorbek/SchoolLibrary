using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;

namespace WebApplication1.Controller;

[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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