using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("comment")]
public class Comment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set; }

    public List<Photo> Photos { get; set; } = [];
    
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? TransactionId { get; set; }
    public ActionTransaction? TransactionAction { get; set; }
    
}