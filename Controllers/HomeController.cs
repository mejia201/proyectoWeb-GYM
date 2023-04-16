using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectoWeb_GYM.Models;
using proyectoWeb_GYM.Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;


namespace proyectoWeb_GYM.Controllers
{

	public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor context;

		
		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Pricing()
        {
            return View();
        }

        [Authentication]
        public IActionResult Dashboard()
        {
			ViewBag.correo = HttpContext.Session.GetString("Usuario");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


		public IActionResult Logout()
		{
           HttpContext.Session.Remove("Usuario");
            return RedirectToAction("Login");
        }

        public IActionResult RecuperarContraseña()
        {
            return View();
        }


		//[HttpPost]
		//public IActionResult SignUp(Usuario oUsuario)
		//{
  //          bool registrado;
  //          string mensaje;

  //          using(SqlConnection cn = new SqlConnection(cadena_conexion))
  //          {
  //              SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
  //              cmd.Parameters.AddWithValue("correo", oUsuario.correo);
  //              cmd.Parameters.AddWithValue("clave", oUsuario.clave);
  //              cmd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output; 
  //              cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output; 

  //              cmd.CommandType = CommandType.StoredProcedure;

  //              cn.Open();

  //              cmd.ExecuteNonQuery();

  //              registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
  //              mensaje = cmd.Parameters["mensaje"].Value.ToString();
  //          }

  //          ViewData["Mensaje"] = mensaje;

  //          if(registrado)
  //          {
  //              return RedirectToAction("Login", "Home");
  //          }
  //          else
  //          {
  //              return View();
  //          }
		//}


		[HttpPost]
		public IActionResult Login(Usuario oUsuario)
		{   
			using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
			{
				SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", oConexion);
				cmd.Parameters.AddWithValue("correo", oUsuario.correo);
				cmd.Parameters.AddWithValue("clave", oUsuario.clave);

				cmd.CommandType = CommandType.StoredProcedure;

				oConexion.Open();

				oUsuario.id_usuario = Convert.ToInt32(cmd.ExecuteScalar());

			}

            if(oUsuario.id_usuario != 0)
            {
				HttpContext.Session.SetString("Usuario", oUsuario.correo);

                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
				ViewData["Mensaje"] = "Usuario no encontrado!";
			    return View();

			}


		}



		[HttpPost]
		public IActionResult RecuperarContraseña(Usuario oUsuario)
		{
			using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
			{
				SqlCommand cmd = new SqlCommand("sp_RecuperarContraseña", oConexion);
				cmd.Parameters.AddWithValue("correo", oUsuario.correo);
				cmd.Parameters.AddWithValue("clave", oUsuario.clave);

				cmd.CommandType = CommandType.StoredProcedure;

				oConexion.Open();

				oUsuario.id_usuario = Convert.ToInt32(cmd.ExecuteScalar());

			}

			if (oUsuario.id_usuario != 0)
			{
				return RedirectToAction("Dashboard", "Home");
			}
			else
			{
				ViewData["Mensaje"] = "Usuario no encontrado!";
				return View();

			}

		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}