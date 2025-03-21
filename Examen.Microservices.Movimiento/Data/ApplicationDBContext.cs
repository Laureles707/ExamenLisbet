﻿using System.Reflection.Emit;
using Examen.Microservices.Movimiento.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen.Microservices.Movimiento.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
           
        }

        public DbSet<MovimientoCab> MovimientoCabs { get; set; }
        public DbSet<MovimientoDet> MovimientoDets { get; set; }
 
    }
}
