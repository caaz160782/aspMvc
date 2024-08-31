using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspMvcNet.Controllers;

public class ErrorController : Controller
{
        [Route("/error/404")]
    public IActionResult Error404()
    {
        return View();
       // return Content("error");
    }
}