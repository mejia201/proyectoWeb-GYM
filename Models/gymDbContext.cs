using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;

namespace proyectoWeb_GYM.Models
{
    public class gymDbContext: DbContext
    {

        public gymDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Membresia> Membresia { get; set; }
    }
}
