using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model;

[Table("photo")]
public class Photo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
    public byte[] Content { get; set; }

    public Guid? CommentId { get; set; }
    public Comment? Comment { get; set; }
}