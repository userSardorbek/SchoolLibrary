using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
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
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnUserDto>))]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnUserDto>("Your request model is invalid"));

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        if (user == null) return Unauthorized("Invalid username!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new ApiResponse<ReturnUserDto>()
        {
            Success = true, Data = new ReturnUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Token = _tokenService.CreateToken(user, roles)
            }
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequestDto userRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnUserDto>("Your request model is invalid"));
        try
        {
            var appUser = new User()
            {
                UserName = userRequestDto.Username,
                FullName = userRequestDto.FullName,
                Email = userRequestDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, userRequestDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(new ApiResponse<ReturnUserDto>(new ReturnUserDto()
                    {
                        UserName = appUser.UserName,
                        FullName = appUser.FullName,
                        Email = appUser.Email
                    }));
                }

                return StatusCode(400,
                    new ApiResponse<IEnumerable<IdentityError>>()
                        { Data = roleResult.Errors, Success = false, Error = "Check data object" });
            }

            return StatusCode(400,
                new ApiResponse<IEnumerable<IdentityError>>()
                    { Data = createdUser.Errors, Success = false, Error = "Check data object" });
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPut("changeUserData")]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnUserDto>))]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnUserDto>("Your request model is invalid"));
        try
        {
            var username = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null) return StatusCode(400, new ApiResponse<ReturnUserDto>("User not found"));

            var exist = await _userManager.Users.AnyAsync(x => x.Email == userDto.Email);
            if (exist && user.Email != userDto.Email)
                return BadRequest(new ApiResponse<ReturnUserDto>($"{userDto.Email} This email is already in use."));

            user.Email = userDto.Email;
            user.UserName = userDto.Username;
            user.FullName = userDto.FullName;

            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
                return BadRequest(new ApiResponse<IEnumerable<IdentityError>>()
                    { Data = identityResult.Errors, Success = false, Error = "Check data object" });

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new ApiResponse<ReturnUserDto>(new ReturnUserDto()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, roles)
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ReturnUserDto>(e.Message));
        }
    }

    [HttpPut("changePassword")]
    [Authorize]
    [ProducesDefaultResponseType(typeof(ApiResponse<ReturnUserDto>))]
    public async Task<IActionResult> ForgotPassword(UserForgotPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ReturnUserDto>("Your request model is invalid"));

        try
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null) return StatusCode(400, new ApiResponse<ReturnUserDto>("User not found"));

            if (dto.NewPassword != dto.ConfirmPassword)
                return StatusCode(400, new ApiResponse<ReturnUserDto>("New password and confirm password did not match"));

            var identityResult = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!identityResult.Succeeded)
            {
                return BadRequest(new ApiResponse<IEnumerable<IdentityError>>()
                {
                    Data = identityResult.Errors,
                    Success = false,
                    Error = "Failed to change password"
                });
            }

            return Ok(new ApiResponse<ReturnUserDto>(new ReturnUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ReturnUserDto>(e.Message));
        }
    }
}