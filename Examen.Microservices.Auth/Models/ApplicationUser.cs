using Microsoft.AspNetCore.Identity;

namespace Examen.Microservices.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
