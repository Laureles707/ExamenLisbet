using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dtos;

namespace Examen.Microservices.Compras.Sevice.IRepository
{
    public interface IProductoRepository
    {
        Task<Producto> CrearProductoAsync(CrearProductoDto crearProductoDto);
        Task<IEnumerable<Producto>> ObtenerProductoAsync();
    }
}
