using Fakebook.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Fakebook.Core.Entities;
using System.Threading.Tasks;

namespace Fakebook.Web.Controllers
{
    public class HomeController : Controller
    {
		private readonly IPostRepository _repository;
		
		public HomeController(IPostRepository repository)
		{
			_repository = repository;
		}
    	public async Task<IActionResult> Index() => View(await _repository.ListAllAsync());
    }
}