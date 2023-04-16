using System.Data.SqlClient;
using Newtonsoft.Json;

namespace proyectoWeb_GYM.Models
{
	public class Usuario
	{
		public int id_usuario { get; set; }

        public string correo { get; set; }
        public string clave { get; set; } 

	}

}
