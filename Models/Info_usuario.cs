using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace proyectoWeb_GYM.Models
{
    public class Info_usuario
    {
        [Key]
        public int id_info_usuario { get; set; }
        public int edad { get; set; }

        public decimal peso { get; set; } 
        public decimal estatura { get; set; }
        public decimal IMC { get; set; }
        public int id_usuario { get; set; }
        public string correo { get; set; }

    }
}
