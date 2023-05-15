using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using proyectoWeb_GYM.Models;
using proyectoWeb_GYM.Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace proyectoWeb_GYM.Controllers
{

	public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor context;
        private readonly gymDbContext _gymContext;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

		public IActionResult Index()
        {
			return View();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

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

        public IActionResult Consejos()
        {
            return View();
        }

        public IActionResult Administracion()
        {
            return View();
        }

        [Authentication]
        public IActionResult Dashboard()
        {
            

            ViewBag.nombre = HttpContext.Session.GetString("Usuario");


            Usuario oUsuario = new Usuario();

            oUsuario.correo = (string)TempData["correoUsuario"];

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


		public IActionResult Logout()
		{

            HttpContext.Session.Remove("Usuario");
           return RedirectToAction("Index");
        }

    

        public IActionResult RecuperarContraseña()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignUp(Usuario usuario)
        {
            bool registrado;
            string mensaje;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oConexion);
                cmd.Parameters.AddWithValue("nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("apellido", usuario.apellido);
                cmd.Parameters.AddWithValue("direccion", usuario.direccion);
                cmd.Parameters.AddWithValue("telefono", usuario.telefono);
                cmd.Parameters.AddWithValue("correo", usuario.correo);
                cmd.Parameters.AddWithValue("clave", usuario.clave);
                cmd.Parameters.AddWithValue("num_cuenta", usuario.num_cuenta);
                cmd.Parameters.AddWithValue("nombre_titular", usuario.nombre_titular);
                cmd.Parameters.AddWithValue("cvv", usuario.cvv);
                cmd.Parameters.AddWithValue("mes", usuario.mes);
                cmd.Parameters.AddWithValue("anio", usuario.anio);
                cmd.Parameters.AddWithValue("id_membresia", usuario.id_membresia);

                cmd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                oConexion.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
                mensaje = cmd.Parameters["mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Dashboard(Info_usuario oUsuario)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_AgregarInformacionPersonal", oConexion);
                cmd.Parameters.AddWithValue("edad", oUsuario.edad);
                cmd.Parameters.AddWithValue("peso", oUsuario.peso);
                cmd.Parameters.AddWithValue("estatura", oUsuario.estatura);
                cmd.Parameters.AddWithValue("correo", oUsuario.correo);

                cmd.CommandType = CommandType.StoredProcedure;

                oConexion.Open();

                oUsuario.id_usuario = Convert.ToInt32(cmd.ExecuteScalar());

                if (oUsuario.id_usuario != 0)
                {

                    TempData["mensaje"] = "Se ha insertado correctamente en la base de datos tus datos.";

                    TempData["edadUsuario"] = oUsuario.edad;
                    TempData["pesoUsuario"] = oUsuario.peso;
                    TempData["estaturaUsuario"] = oUsuario.estatura;

                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {

                    TempData["mensaje"] = "Ocurrió un error al insertar en la base de datos, este usuario ya completó su perfil, comuníquese con el administrador para poder editarlo";

                    return RedirectToAction("Dashboard", "Home");
                }

            }

        }
            
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

                oUsuario.id_usuario  = Convert.ToInt32(cmd.ExecuteScalar());

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    if (oUsuario.id_usuario != 0)
                    { 

                    int id = (int)reader["id_usuario"];
                    string nombre = (string)reader["nombre"];
                    string apellido = (string)reader["apellido"];
                    string correo = (string)reader["correo"];

                    oUsuario.id_usuario = id;
                    oUsuario.nombre = nombre;
                    oUsuario.apellido = apellido;
                    oUsuario.correo = correo;


                    }
                    else
                    {
                        ViewData["Mensaje"] = "Usuario no encontrado!";
                        return View();
                    }

                   
                }

                reader.Close();
                oConexion.Close();


            }

            if(oUsuario.id_usuario != 0)
            {


               if(oUsuario.correo == "admin@gmail.com")
                {
                    return RedirectToAction("Administracion", "Home");
                }

                HttpContext.Session.SetString("Usuario", oUsuario.nombre + " " + oUsuario.apellido);
                

                TempData["correoUsuario"] = oUsuario.correo;

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