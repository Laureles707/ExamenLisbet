using AutoMapper;
using Examen.Microservices.Compras.Data;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Compras.Sevice.IRepository;

namespace Examen.Microservices.Compras.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public ProductoRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

       

        public async Task<Producto> CrearProductoAsync(CrearProductoDto crearProductoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(crearProductoDto);
                await _db.Productos.AddAsync(producto);
                await _db.SaveChangesAsync();
                return producto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Producto>> ObtenerProductoAsync()
        {
            try
            {
                return await Task.FromResult(_db.Productos.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    
}
