
using Examen.Microservices.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Examen.Microservices.Auth.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var hasher = new PasswordHasher<IdentityUser>();

            //var adminUser = new IdentityUser
            //{
            //    Id = "1", // ID debe ser único y en string
            //    UserName = "admin@example.com",
            //    NormalizedUserName = "ADMIN@EXAMPLE.COM",
            //    Email = "admin@example.com",
            //    NormalizedEmail = "ADMIN@EXAMPLE.COM",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "Admin@123"),
            //    SecurityStamp = string.Empty
            //};

            //modelBuilder.Entity<IdentityUser>().HasData(adminUser);


        }

    }
}
