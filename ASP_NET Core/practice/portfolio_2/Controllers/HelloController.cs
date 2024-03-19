using Microsoft.AspNetCore.Mvc;
namespace portfolio_2.Controllers;

public class HelloController : Controller
{
    [HttpGet("")]
    public ViewResult Index()
    {
        return View("Index");
    }
    [HttpGet("projects")]
    public ViewResult Projects()
    {
        return View("Projects");
    }
    [HttpGet("contact")]
    public ViewResult Contact()
    {
        return View("Contact");
    }
}