#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BrightIdeas.Models;
public class Post
{
    [Key]
    public int PostId { get; set; }
    [Required]
    public string Idea { get; set; }

    public int UserId { get; set; }
    public User? Poster {get; set; }
    public List<Like> UserWhoLiked { get; set; } = new List<Like>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
                
