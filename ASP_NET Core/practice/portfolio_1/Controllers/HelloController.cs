using Microsoft.AspNetCore.Mvc;
namespace portfolio_1.Controllers;

public class HelloController : Controller
{
    [HttpGet("")]
    public string index()
    {
        return "This is my index";
    }

    [HttpGet("projects")]
    public string projects()
    {
        return "these are my projects!";
    }

    [HttpGet("contact")]
    public string contact()
    {
        return "this is my contact!";
    }


}