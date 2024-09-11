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

    [Route("/Example")]
    public IActionResult Example()
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

}
