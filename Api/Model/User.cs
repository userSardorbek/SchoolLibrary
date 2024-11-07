using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
[Table("users")]
public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Required]
    [MinLength(6)]
    [MaxLength(70)]
    public string Username { get; set; }

    [Required]
    [MaxLength(101)]
    [MinLength(8)]
    public string Password { get; set; }

    [Required]
    public string FullName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}