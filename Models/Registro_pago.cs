using System.ComponentModel.DataAnnotations;

namespace proyectoWeb_GYM.Models
{
	public class Registro_pago
	{
		[Key]
		public int id_registro_pago { get; set; }
		public int id_usuario { get; set; }
		public string id_pago { get; set; }
	}
}
