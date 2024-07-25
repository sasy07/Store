using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ProductTypeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}