using Examen.Web.Models;

namespace Examen.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer=true);
    }
}
