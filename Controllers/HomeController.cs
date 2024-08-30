using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspMvcNet.Models;

namespace AspMvcNet.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("/rutasExp")]
    public IActionResult rutasExp()
    {
        return Content("asi son la rutas en asp");
    }

    [Route("/parametros/{id}/{name}")]
    public IActionResult parametros(int id, string name)
    {
        return Content($"id={id} | name = {name}");
    }
    // private readonly ILogger<HomeController> _logger;

    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
