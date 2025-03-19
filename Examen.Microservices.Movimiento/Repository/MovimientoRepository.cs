using AutoMapper;
using Examen.Microservices.Movimiento.Data;
using Examen.Microservices.Movimiento.Models.Dto;
using Examen.Microservices.Movimiento.Models.Dtos;
using Examen.Microservices.Movimiento.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Examen.Microservices.Movimiento.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public MovimientoRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ResponseDto> GetKardexAsync(int idProducto)
        {
            var response = new ResponseDto();
            try
            {
                var objListMov = await(
                    from c in _db.MovimientoCabs
                    join d in _db.MovimientoDets on c.Id_MovimientoCab equals d.Id_movimientocab into detalles
                    from mc in detalles.DefaultIfEmpty() // Left Join
                    where mc.Id_Producto == idProducto
                    select new MovimientoProductoDto
                    {
                        fecha_registro = c.Fec_registro.ToString("dd/MM/yyyy HH:mm:ss"), // Corrección de formato
                        tipo_movimiento = (c.Id_TipoMovimiento == 1) ? "Entrada" : "Salida",
                        cantidad = mc.Cantidad
                    }
                ).ToListAsync();

                response.Result = _mapper.Map<IEnumerable<MovimientoProductoDto>>(objListMov);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
