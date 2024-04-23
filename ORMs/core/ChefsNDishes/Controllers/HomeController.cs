using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

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
        ViewBag.AllChefs = _context.Chefs.Include(d => d.ChefDishes).ToList();
        // ViewBag.DishesCount = _context.Chefs.Select(c => c.ChefDishes.Count());
        
        return View();
    }
    [HttpGet("/dishes")]
    public IActionResult Dishes()
    {   
        ViewBag.AllDishes = _context.Dishes.ToList();
        ViewBag.ChefsDish = _context.Chefs.ToList();
        // ViewBag.AllChefs = _context.Chefs.Include(d => d.ChefDishes).ToList();
        // ViewBag.DishesCount = _context.Chefs.Select(c => c.ChefDishes.Count());
        
        return View("Dishes");
    }

    [HttpGet("chefs/new")]
    public IActionResult ChefForm()
    {
        return View("AddChef");
    }

    [HttpGet("dishes/new")]
    public IActionResult DishForm()
    {
        ViewBag.ChefsDish = _context.Chefs.ToList();
        return View("AddDish");
    }
    public IActionResult AddChef( Chef newChef)
    {
        if(ModelState.IsValid)

        {
            _context.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else{
            return View();
        }

    }
    public IActionResult AddDish ( Dish newDish)
    {   
        // ViewBag.ChefsDish = _context.Chefs.ToList();
        if(ModelState.IsValid)

        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Dishes");
        }
        else{
            return View();
        }

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
