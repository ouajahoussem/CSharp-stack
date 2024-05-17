using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers;

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
        return View();
    }
    [HttpPost("user/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();

            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Index");
        }
        else
        {
            return View("Index");
        }
    }

    [HttpPost("user/login")]
    public IActionResult LoginUser(LoginUser loginUser)
    {
        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == loginUser.LogEmail);

            if (userInDb == null)
            {
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                return View("Index");
            }
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LogPassword);                                    // Result can be compared to 0 for failure        
            if (result == 0)
            {
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                return View("Index");
            }
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return RedirectToAction("Recipes");
        }
        else
        {
            return View("Index");
        }
    }


    [HttpGet("recipes")]
    public IActionResult Recipes()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            List<Recipe> AllRecipes = _context.Recipes.Include(a => a.Reciper).Include(x => x.UserWhoRated).ThenInclude(x => x.UserRating).ThenInclude(u => u.RecipesRated).ToList();
            User? userIndb = _context.Users.Include(u => u.RecipesRated).ToList().FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.LogedInUser = userIndb;

            return View(AllRecipes);

        }

    }

    [HttpGet("/recipes/new")]
    public IActionResult AddRecipeForm()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }
        User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
        ViewBag.LogedInUser = userIndb;

        return View();
    }
    
    public IActionResult AddRecipe(Recipe newRecipe)
    {
        if (ModelState.IsValid)
        {
            newRecipe.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newRecipe);
            _context.SaveChanges();
            return RedirectToAction("ShowRecipe",newRecipe);
        }
        else
        {
            User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.LogedInUser = userIndb;
            return View("AddRecipeForm");
        }
    }


    [HttpGet("recipes/{recipeId}")]
    public IActionResult ShowRecipe(int recipeId)
    {

        User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
        ViewBag.LogedInUser = userIndb;
        Recipe? RecipeToShow = _context.Recipes.Include(s => s.Reciper).Include(d => d.UserWhoRated).ThenInclude(u => u.UserRating).ToList().FirstOrDefault(a => a.RecipeId == recipeId);
        return View(RecipeToShow);
    }

    [HttpGet("recipes/{recipeId}/edit")]
    public IActionResult EditRecipe(int recipeId)
    {
        Recipe RecipetoEdit = _context.Recipes.SingleOrDefault(d => d.RecipeId == recipeId);
        return View(RecipetoEdit);
    }

    public IActionResult UpdateRecipe(int recipeId, Recipe updatedRecipe)
    {
        Recipe RecipeToUpdate = _context.Recipes.FirstOrDefault(r => r.RecipeId == recipeId)!;
        if (ModelState.IsValid)
        {
            RecipeToUpdate.Title = updatedRecipe.Title;
            RecipeToUpdate.IngredientOne = updatedRecipe.IngredientOne;
            RecipeToUpdate.IngredientTwo = updatedRecipe.IngredientTwo;
            RecipeToUpdate.IngredientThree = updatedRecipe.IngredientThree;
            RecipeToUpdate.IngredientFour = updatedRecipe.IngredientFour;
            RecipeToUpdate.IngredientFive = updatedRecipe.IngredientFive;
            RecipeToUpdate.Introductions = updatedRecipe.Introductions;
            RecipeToUpdate.Vegetarian = updatedRecipe.Vegetarian;
            RecipeToUpdate.GlutenFree = updatedRecipe.GlutenFree;
            RecipeToUpdate.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Recipes");
        };
        return View("EditRecipe", RecipeToUpdate);
    }

    public IActionResult RecipeRate(int userId, int recipeId)
    {

        Rate newRate = new Rate()
        {
            UserId = userId,
            RecipeId = recipeId
        };
        _context.Rates.Add(newRate);
        _context.SaveChanges();
        return RedirectToAction("ShowRecipe", new { recipeId = newRate.RecipeId });
    }

    [HttpPost]
public IActionResult DeleteRate(int rateId)
{
    
    var rateToRemove = _context.Rates.FirstOrDefault(r => r.RateId == rateId);

    if (rateToRemove == null)
    {
        
        return NotFound();
    }

    
    _context.Rates.Remove(rateToRemove);
    _context.SaveChanges();

    
    return RedirectToAction("Recipes", new { recipeId = rateToRemove.RecipeId });
}


    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
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
