using Examen.Microservices.Auth.Models.Dto;

namespace Examen.Microservices.Auth.Services.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    
    }
}
