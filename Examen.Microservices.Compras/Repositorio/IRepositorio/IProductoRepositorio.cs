
using Examen.Microservices.Compras.Models;

namespace Examen.Microservices.Compras.Repositorio.IRepositorio
{
    public interface IProductoRepositorio
    {
        ICollection<Producto> GetProductos();
        Producto GetProducto(int Productoid);
        bool ExisteProducto(string nombre_producto);
        bool CrearProducto(Producto producto);
    }
}
