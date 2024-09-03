using Microsoft.AspNetCore.Mvc;

namespace AspMvcNet.Controllers;

public class RazorController : Controller
{
    [Route("/razor")]
    public IActionResult Index()
    {
        ViewData["id"]="2";
        ViewData["name"]="Pedro navajas";
        ViewBag.id=3;
        ViewBag.TeamName="Cocoleros";
        return View();
    }
}

