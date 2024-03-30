using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    [HttpPost("login")]
    public IActionResult Login(string UserName)
    {
        HttpContext.Session.SetString("Username", UserName);

        return RedirectToAction("Dashboard");
    }


    [HttpPost("logout")]
    public IActionResult logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpPost("add")]
    public IActionResult Add()
    {
        int num = (int)(ViewBag.Num = 22);
        num += 1;
        ViewBag.Num = num;
        return RedirectToAction("Dashboard");
    }

    public IActionResult Index()
    {


        return View();
    }

    public IActionResult Dashboard()
    {
        string? InSession = HttpContext.Session.GetString("Username");
        if (InSession == null)
        {
            return RedirectToAction("Index");
        }
        ViewBag.Num = 22;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
