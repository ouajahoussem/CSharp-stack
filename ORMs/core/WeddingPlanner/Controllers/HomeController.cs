using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace WeddingPlanner.Controllers;

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
            // // Updating our newUser's password to a hashed version  
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
            // Handle else
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return RedirectToAction("AllWeddings");
        }
        else
        {
            return View("Index");
        }
    }

    [HttpGet("weddings")]
    public IActionResult AllWeddings()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            List<Wedding> AllWeddings = _context.Weddings.Include(a => a.Wedders).Include(x => x.UserAttending).ThenInclude(x => x.UserAsGuest).ToList();
            User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.LogedInUser = userIndb;
            return View(AllWeddings);

        }
        // ViewBag.NotLogedIn = false;
        // ViewBag.Top = _context.Songs.Include(s => s.Artist).Include(aa => aa.UserHowLiked).OrderByDescending(a => a.UserHowLiked.Count).Take(3).ToList();
    }


    [HttpGet("/weddings/new")]
    public IActionResult AddWeddingForm()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }
        User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
        ViewBag.LogedInUser = userIndb;
        // ViewBag.NotLogedIn = false;
        return View();
    }
    public IActionResult AddWedding(Wedding newWedd)
    {
        if (ModelState.IsValid)
        {
            newWedd.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newWedd);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }
        else
        {
            User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.LogedInUser = userIndb;
            return View("AddWeddingForm");
        }
    }


    public IActionResult GuestRSVP(int userId, int weddingId)
    {

        Guest newGuest = new Guest()
        {
            UserId = userId,
            WeddingId = weddingId
        };
        _context.Guests.Add(newGuest);
        _context.SaveChanges();
        return RedirectToAction("AllWeddings");
    }
    public IActionResult GuestUnRSVP(int userId, int weddingId)
    {
        Guest? guestToRemove = _context.Guests.SingleOrDefault(g => g.UserId == userId && g.WeddingId == weddingId);
        _context.Guests.Remove(guestToRemove);
        _context.SaveChanges();
        return RedirectToAction("AllWeddings");
    }
    
    

    [HttpGet("weddings/{weddingId}")]
    public IActionResult ShowWedding(int weddingId)
    {
        
        User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
        ViewBag.LogedInUser = userIndb;
        Wedding? WeddingToShow = _context.Weddings.Include(s => s.Wedders).Include(d => d.UserAttending).ThenInclude(u => u.UserAsGuest).ToList().FirstOrDefault(a => a.WeddingId == weddingId);
        return View(WeddingToShow);
    }
    


    [HttpGet("weddings/delete/{weddingId}")]
    public IActionResult DeleteWedd(int weddingId)
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("logout");
        }
        else
        {
            Wedding? WeddingToDelete = _context.Weddings.SingleOrDefault(q => q.WeddingId == weddingId);
            _context.Weddings.Remove(WeddingToDelete);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }

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


