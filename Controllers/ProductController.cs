using Microsoft.AspNetCore.Mvc;

namespace SampleApplication.Controllers
{
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}