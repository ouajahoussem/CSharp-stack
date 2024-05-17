#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BrightIdeas.Models;
public class Like
{
    [Key]
    public int LikeId { get; set; }

    public int UserId { get; set; }
    public int PostId { get; set; }
    public User? UserLikes {get; set; }
    public Post? PostLikes { get; set; }
}
                
