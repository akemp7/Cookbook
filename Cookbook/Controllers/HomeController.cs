using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}