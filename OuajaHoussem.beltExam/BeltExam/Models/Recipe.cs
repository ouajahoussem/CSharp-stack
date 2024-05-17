#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BeltExam.Models;
public class Recipe
{
    [Key]
    public int RecipeId { get; set; }
    [Required]
    public string Title {get; set; }
    [Required]
    [Display(Name = "ingredient 1:")]
    public string IngredientOne {get; set; }
    [Display(Name = "ingredient 2:")]
    [Required]
    public string IngredientTwo {get; set; }
    [Display(Name = "ingredient 3:")]
    [Required]
    public string IngredientThree {get; set; }
    [Display(Name = "ingredient 4:")]
    [Required]
    public string IngredientFour {get; set; }
    [Display(Name = "ingredient 5:")]
    [Required]
    public string IngredientFive {get; set; }
    
    [Required]
    [Display(Name = "Introductions:")]
    public string Introductions { get; set; }
    [Required]
    public bool Vegetarian {get; set; } = false;
    [Required]
    [Display(Name = "Gluten Free")]
    public bool GlutenFree { get; set; } = false;

    public int UserId { get; set; }
    public User? Reciper {get; set; }
    public List<Rate> UserWhoRated { get; set; } = new List<Rate>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}