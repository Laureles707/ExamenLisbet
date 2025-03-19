using Examen.Web.Models;

namespace Examen.Web.Service.IService
{
    public interface IProductoService
    {
        Task<ResponseDto?> GetProductos();
        Task<ResponseDto?> CrearProducto(ProductoRequestDto producto);
    }
}
