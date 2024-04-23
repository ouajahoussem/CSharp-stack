#pragma warning disable CS8618
namespace ChefsNDishes.Models;


public class MyViewModel
{
    public Chef Chef { get; set; }
    public List<Chef> AllChefs { get; set; }
    public Dish Dish { get; set; }
    public List<Dish> AllDishes { get; set; }

}