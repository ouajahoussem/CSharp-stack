using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CruDelicious.Models;
using System.Diagnostics.Eventing.Reader;

namespace CruDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;
    }

    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
        return View(AllDishes);
    }
    [HttpGet("dishes/new")]
    public IActionResult AddDishForm()
    {
        return View();
    }

    public IActionResult AddDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("AddDishForm");
        }
    }
    [HttpGet("dishes/{dishId}")]
    public IActionResult DishById(int dishId)
    {
        Dish oneDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
        return View(oneDish);
    }

    public IActionResult DeleteDish(int dishId)
    {
        Dish DishToDelete = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);

        _context.Dishes.Remove(DishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("dishes/{dishId}/edit")]
    public IActionResult EditDish(int dishId)
    {
        Dish dishToEdit = _context.Dishes.SingleOrDefault(d => d.DishId == dishId);
        return View(dishToEdit);
    }
    
    public IActionResult UpdateDish(int dishId, Dish UpdatedDish)
    {
        Dish DishToUpdate = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
        if (ModelState.IsValid)
        {
            DishToUpdate.Name = UpdatedDish.Name;
            DishToUpdate.Chef = UpdatedDish.Chef;
            DishToUpdate.Tastiness = UpdatedDish.Tastiness;
            DishToUpdate.Calories = UpdatedDish.Calories;
            DishToUpdate.Description = UpdatedDish.Description;
            DishToUpdate.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("DishById", new { dishId });
        }
        return View("EditDish", DishToUpdate);
    }











    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
