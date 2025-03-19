using Examen.Microservices.Compras.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen.Microservices.Venta.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<VentaCab> VentaCabs { get; set; }

        public DbSet<VentaDet> VentaDets { get; set; }
        public DbSet<MovimientoCab> MovimientoCabs { get; set; }
        public DbSet<MovimientoDet> MovimientoDets { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        


        }


   
    }
}
