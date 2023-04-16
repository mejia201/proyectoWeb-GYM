using Microsoft.AspNetCore.Mvc;

namespace proyectoWeb_GYM.Controllers
{
	public class PlanController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Planes()
		{
			return View();
		}
	}
}
