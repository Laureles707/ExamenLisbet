using Examen.Web.Models;

namespace Examen.Web.Service.IService
{
    public interface IMovimientoService
    {

        Task<ResponseDto?> GetMovimientoProducto(int idProducto);
    }
}
