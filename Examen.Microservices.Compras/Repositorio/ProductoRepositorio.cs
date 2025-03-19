using Examen.Microservices.Compras.Data;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Repositorio.IRepositorio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Examen.Microservices.Compras.Repositorio
{
    public class ProductoRepositorio: IProductoRepositorio
    {
        private readonly ApplicationDBContext _bd;

        public ProductoRepositorio(ApplicationDBContext bd)
        {
            _bd = bd;
        }

        public bool CrearProducto(Producto producto)
        {
            producto.Fec_registro = DateTime.Now;
            _bd.Productos.Add(producto);
            return Guardar();
        }

        public bool ExisteProducto(string Producto_nombre)
        {
            return _bd.Productos.Any(c => c.Nombre_producto == Producto_nombre);
        }

        public bool GetProducto(string Producto_nombre)
        {
            bool valor = _bd.Productos.Any(c => c.Nombre_producto.ToLower().Trim() == Producto_nombre.ToLower().Trim());
            return valor;
        }

        public Producto GetProducto(int Productoid)
        {
            return _bd.Productos.FirstOrDefault(c => c.Id_Producto == Productoid);
        }

        public ICollection<Producto> GetProductos()
        {
            return _bd.Productos.OrderBy(c => c.Fec_registro).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
