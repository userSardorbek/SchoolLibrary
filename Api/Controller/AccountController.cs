using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, ITokenService tokenService,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        if (user == null) return Unauthorized("Invalid username!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
        return Ok(new ApiResponse<NewUserDto>()
        {
            Success = true, Data = new NewUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            }
        });
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

                return StatusCode(500,
                    new ApiResponse<IEnumerable<IdentityError>>()
                        { Data = roleResult.Errors, Success = false, Error = "Check data object" });
            }

            return StatusCode(500,
                new ApiResponse<IEnumerable<IdentityError>>()
                    { Data = createdUser.Errors, Success = false, Error = "Check data object" });
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}