using Microsoft.AspNetCore.Mvc;

namespace proyectoWeb_GYM.Controllers
{
	public class UsuarioController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
