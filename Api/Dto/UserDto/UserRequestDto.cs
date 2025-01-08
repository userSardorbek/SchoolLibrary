using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto;

public class UserRequestDto
{
    [Required]
    public string Username { get; set; }
    public string? Password { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}