#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BeltExam.Models;
public class Rate
{
    [Key]
    public int RateId { get; set; }
    
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    public User? UserRating { get; set; }    
    public Recipe? RecipeRatings { get; set; }

    
}