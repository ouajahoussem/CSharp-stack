using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

using LogAndReg.Models;

namespace LogAndReg.Controllers;

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
            // hash our password
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            // Updating our newUser's password to a hashed version  
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
            // If initial ModelState is valid, query for a user with the provided email        
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == loginUser.LogEmail);
            // If no user exists with the provided email        
            if (userInDb == null)
            {
                // Add an error to ModelState and return to View!            
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                return View("Index");
            }
            // Otherwise, we have a user, now we need to check their password                 
            // Initialize hasher object        
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            // Verify provided password against hash stored in db        
            var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LogPassword);                                    // Result can be compared to 0 for failure        
            if (result == 0)
            {
                // Handle failure (this should be similar to how "existing email" is handled)    
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                return View("Index");
            }
            // Handle success (this should route to an internal page)  
            else
            {
                // Handle else
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("Success");

            }
        }
        else
        {
            return View("Index");
        }
    }





    [SessionCheck]
    [HttpGet("success")]
    public IActionResult Success()
    {
        return View();
    }


    [HttpPost("logout")]
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


public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if (userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}


