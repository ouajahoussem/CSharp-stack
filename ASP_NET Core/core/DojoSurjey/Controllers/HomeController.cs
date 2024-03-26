using Microsoft.AspNetCore.Mvc;
namespace DojoSurjey.Controllers;
public class HomeController : Controller
{
    [HttpGet("")]
    public ViewResult Index()
    {
        return View("Index");
    }

    [HttpPost("process")]
    public IActionResult Create(string Name, string Location, string FavoriteLanguage, string Comment)
    {
        if(string.IsNullOrEmpty(Name))
        {
            return RedirectToAction("Index");
        }
        else
        {
        
            return RedirectToAction("Results", new { Name = Name, Location = Location, FavoriteLanguage = FavoriteLanguage,     Comment = Comment});
        }
    }


    [HttpGet("results")]
    public IActionResult Results(string Name, string Location, string FavoriteLanguage, string Comment)
    {
            ViewBag.Name = Name;
            Console.WriteLine(ViewBag.Name);
            ViewBag.Location = Location;
            Console.WriteLine(ViewBag.Location);
            ViewBag.FavoriteLanguage = FavoriteLanguage;
            Console.WriteLine(ViewBag.FavoriteLanguage);
            ViewBag.Comment = Comment;
            Console.WriteLine(ViewBag.Comment);
        return View("Results");
    }
}