using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}