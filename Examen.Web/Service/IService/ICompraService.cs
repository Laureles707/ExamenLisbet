using Examen.Web.Models;

namespace Examen.Web.Service.IService
{
    public interface ICompraService
    {
        Task<ResponseDto?> GetCompras();
        Task<ResponseDto?> CrearCompra(CompraDto compra);
    }
}
