using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto.BookDto;

public class BookEditDto
{
    [Required]
    public string Title { get; set; } 
    [Required]
    public string Author { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public bool Availability { get; set; } 
    [Required]
    public int[] GenreId { get; set; }
}