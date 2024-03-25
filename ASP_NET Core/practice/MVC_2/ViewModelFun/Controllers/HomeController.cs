using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ViewModelFun.Models;

namespace ViewModelFun.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string message = "shdsjhdshdsjhjsdhsj dskdjskdjsdjsdjks dsdsdhjsdskdjsldls dlsdlsdlskdlskdls";
        return View("Index", message);
    }
    [HttpGet("numbers")]
    public IActionResult Numbers()
    {
        List<int> someNumbers = new List<int>() {1,2,10,21,8,7,3};
        return View("Numbers", someNumbers);
        
    }
    [HttpGet("user")]
    public IActionResult OneUser()
    {
        User OneUser = new User
        {
            FirstName = "Neil",
            LastName = "Gaiman"
            
        };
        return View("User",OneUser);
    }
    [HttpGet("users")]
    public IActionResult Users()
    {
        List<User> ListOfUsers = new List<User>()
        {
            new User() {FirstName = "Neil", LastName="Gaiman"},
            new User() {FirstName = "Terry", LastName="Pratchet"},
            new User() {FirstName = "Jane", LastName="Austen"},
            new User() {FirstName = "Stephen", LastName="King"},
            new User() {FirstName = "Mary", LastName="Shelley"},
        };
        return View("Users",ListOfUsers);
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
