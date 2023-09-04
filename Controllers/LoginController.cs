using Microsoft.AspNetCore.Mvc;

namespace keycloak.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return Redirect("https://localhost:3000/admin/Test/console/");
    }
}