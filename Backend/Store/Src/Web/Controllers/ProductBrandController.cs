using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ProductBrandController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}