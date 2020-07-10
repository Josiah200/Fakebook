using Microsoft.AspNetCore.Mvc;

namespace FakeBook.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}