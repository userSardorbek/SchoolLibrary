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
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    [ProducesDefaultResponseType(typeof(ApiResponse<NewUserDto>))]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var appUser = new User()
            {
                UserName = registerDto.Username,
                FullName = registerDto.FullName,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(new ApiResponse<NewUserDto>(new NewUserDto()
                    {
                        UserName = appUser.UserName, 
                        Email = appUser.Email, 
                        Token = _tokenService.CreateToken(appUser)
                    }));
                }
                return StatusCode(500, new ApiResponse<IEnumerable<IdentityError>>(){ Data = roleResult.Errors, Success = false, Error = "Check data object"});
            }
            return StatusCode(500, new ApiResponse<IEnumerable<IdentityError>>(){ Data = createdUser.Errors, Success = false, Error = "Check data object"});
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}