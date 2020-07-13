using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}