using Examen.Microservices.Auth.Models;

namespace Examen.Microservices.Auth.Services.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
