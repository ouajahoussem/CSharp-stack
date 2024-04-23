#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required]
    [Display(Name = "Name of Dish")]
    public string Name { get; set; }
    [Required]
    [Display(Name = "# of Calories")]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0")]
    public int Calories { get; set; }
    [Required]
    [Range(1,5)]
    public int  Tastiness { get; set ;}
    [Display(Name = "Chefs Name")]
    public Chef? Chef { get; set; }
    //! navigation property that let's us connect to pet Model
    public int ChefId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}