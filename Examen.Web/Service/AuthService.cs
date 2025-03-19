using Examen.Web.Models;
using Examen.Web.Service.IService;
using Examen.Web.Utility;

namespace Examen.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.GatewayAPI + "/gateway/login"
                
            }, withBearer: false);
        }
    }
}
