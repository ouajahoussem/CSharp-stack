using Microsoft.AspNetCore.Mvc;
namespace razorFun.Controllers; 
public class HelloController : Controller
{
    [HttpGet("")]
    public ViewResult Index()
    {
        return View("Index");
    }
}