using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using keycloak.Models;
using Microsoft.AspNetCore.Authorization;

namespace keycloak.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    // [Authorize(Roles = "Administradors")]
    public IActionResult Index()
    {
       return View();
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
