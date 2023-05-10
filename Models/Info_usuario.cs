using System.Reflection.Metadata;

namespace proyectoWeb_GYM.Models
{
    public class Info_usuario
    {
        public int id_info_usuario { get; set; }
        public int edad { get; set; }

        public decimal peso { get; set; } 
        public decimal estatura { get; set; }
        public decimal IMC { get; set; }
        public Blob foto { get; set; }
        public int id_usuario { get; set; }
        public string correo { get; set; }

    }
}
