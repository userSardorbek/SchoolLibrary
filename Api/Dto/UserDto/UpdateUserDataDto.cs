using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto;

public class UpdateUserDataDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}