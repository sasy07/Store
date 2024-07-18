using Microsoft.AspNetCore.Mvc;

namespace Web.Common;

public class BaseApiController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}