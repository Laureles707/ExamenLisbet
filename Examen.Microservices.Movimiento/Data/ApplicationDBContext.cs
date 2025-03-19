using System.Reflection.Emit;
using Examen.Microservices.Movimiento.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen.Microservices.Movimiento.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
           
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<CompraCab> CompraCabs { get; set; }

        public DbSet<CompraDet> CompraDets { get; set; }
        public DbSet<MovimientoCab> MovimientoCabs { get; set; }
        public DbSet<MovimientoDet> MovimientoDets { get; set; }
 
    }
}
