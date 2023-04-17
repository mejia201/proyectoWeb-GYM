using System.Data.SqlClient;
using System.Security.Principal;
using Newtonsoft.Json;

namespace proyectoWeb_GYM.Models
{
	public class Usuario
	{
		public int id_usuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
	    public string apellido { get; set; }
	    public string direccion { get; set; }
		public string telefono { get; set; }
	    public string num_cuenta { get; set; }
	    public string nombre_titular { get; set; }
	    public string cvv { get; set; }

		public int mes { get; set; }
		public int anio { get; set; }
        public Membresia oMembresia { get; set; }
        public bool estado { get; set; }

    }

}
