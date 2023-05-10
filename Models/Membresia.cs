using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace proyectoWeb_GYM.Models
{
    public class Membresia
    {

    [Key]
        [Display(Name = "ID Membresia")]
        public int id_membresia { get; set; }

        [Display(Name = "Nombre")]
        public string? nombre_membresia { get; set; }
        [Display(Name = "Precio")]
        public double precio { get; set; }

        [Display(Name = "Vendidas")]
        public int vendidas { get; set; }

    }

    




}
