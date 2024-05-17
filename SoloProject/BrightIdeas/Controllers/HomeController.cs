using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrightIdeas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrightIdeas.Controllers;

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
            return RedirectToAction("Dashboard");
        }
        else
        {
            return View("Index");
        }
    }
    [HttpGet("bright_ideas")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            MyViewModel MyModel = new MyViewModel()
            {

                AllPosts = _context.Posts.Include(a => a.Poster).Include(d => d.UserWhoLiked).Include(x => x.UserWhoLiked).ThenInclude(x => x.UserLikes).ThenInclude(u =>u.PostsLiked).ToList()
            };
            User? UserIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.LoggedInUser = UserIndb;
            return View(MyModel);

        }

    }

    public IActionResult CreatePost(Post newPost)
    {
        if (ModelState.IsValid)
        {
            newPost.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newPost);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        else
        {
            MyViewModel MyModel = new MyViewModel()
            {

                AllPosts = _context.Posts.Include(a => a.Poster).Include(x => x.UserWhoLiked).ThenInclude(x => x.UserLikes).ThenInclude(u => u.PostsLiked).ToList()
            };
            User? UserIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.LoggedInUser = UserIndb;
            return View("Dashboard", MyModel);

        }
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpGet("users/{userId}")]
    public IActionResult ShowUser(int userId)
    {
        User? userIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
        ViewBag.LoggedInUser = userIndb;
        User? ShowUser = _context.Users.Include(p => p.IdeasPosted).Include(c => c.PostsLiked).FirstOrDefault(a => a.UserId == userId);
        return View(ShowUser);
    }
    [HttpGet("bright_ideas/{postId}")]
    public IActionResult ShowPost(int postId)
    {
        MyViewModel MyModel = new MyViewModel()
        {
            OnePost = _context.Posts.Include(a => a.Poster).Include(x => x.UserWhoLiked).ThenInclude(t=>t.UserLikes).ToList().FirstOrDefault(o => o.PostId == postId),
                
        };
        User? UserIndb = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
        ViewBag.LoggedInUser = UserIndb;
        return View(MyModel);
    }
    public IActionResult LikePost(int userId, int postId)
    {

        Like newGuest = new Like()
        {
            UserId = userId,
            PostId = postId
        };
        _context.Likes.Add(newGuest);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }
    public IActionResult dislikePost(int userId, int postId)
    {
        Like? LiketoRemove = _context.Likes.SingleOrDefault(g => g.UserId == userId && g.PostId == postId);
        _context.Likes.Remove(LiketoRemove);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    
    [HttpGet("bright_ideas/delete/{postId}")]
    public IActionResult DeletePost(int postId)
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("logout");
        }
        else
        {
            Post? PosttoDelete = _context.Posts.SingleOrDefault(q => q.PostId == postId);
            _context.Posts.Remove(PosttoDelete);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
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
