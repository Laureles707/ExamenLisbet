using Examen.Web.Models;

namespace Examen.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
