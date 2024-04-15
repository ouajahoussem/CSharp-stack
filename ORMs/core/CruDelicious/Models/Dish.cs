#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CruDelicious.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    
    public string Name { get; set; } 
    
    public string Chef { get; set; }
    
    [Range(1,5)]
    public int Tastiness { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0")]
    public int Calories { get; set; }
    
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

