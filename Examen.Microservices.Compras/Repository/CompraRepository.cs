using AutoMapper;
using Examen.Microservices.Compras.Data;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Compras.Repository.IRepository;

namespace Examen.Microservices.Compras.Repository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public CompraRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ResponseDto> CrearCompraAsync(CrearCompraDto crearCompraDto)
        {
            var response = new ResponseDto();
            try
            {
                // Crear Cabecera de la Compra
                var compraCabecera = new CompraCab
                {
                    FecRegistro = DateTime.Now,
                    SubTotal = crearCompraDto.compraDet.Sum(d => d.Precio * d.Cantidad),
                    Igv = crearCompraDto.compraDet.Sum(d => d.Precio * d.Cantidad) * 0.18m,
                    Total = crearCompraDto.compraDet.Sum(d => d.Precio * d.Cantidad) * 1.18m
                };

                await _db.CompraCabs.AddAsync(compraCabecera);
                await _db.SaveChangesAsync();

                var id_cab = compraCabecera.Id_CompraCab;

                // Crear Detalles de la Compra
                var compraDetalles = crearCompraDto.compraDet.Select(d => new CompraDet
                {
                    Id_Producto = d.Id_producto,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                    Sub_Total = d.Precio * d.Cantidad,
                    Igv = (d.Precio * d.Cantidad) * 0.18m,
                    Total = (d.Precio * d.Cantidad) * 1.18m,
                    Id_CompraCab = id_cab
                }).ToList();

                await _db.CompraDets.AddRangeAsync(compraDetalles);
                await _db.SaveChangesAsync();

                // Crear Cabecera del Movimiento
                var movimientoCabecera = new MovimientoCab
                {
                    Fec_registro = DateTime.Now,
                    Id_TipoMovimiento = 1,
                    Id_DocumentoOrigen = id_cab
                };

                await _db.MovimientoCabs.AddAsync(movimientoCabecera);
                await _db.SaveChangesAsync();

                var movCab_Id = movimientoCabecera.Id_MovimientoCab;

                // Procesar cada Detalle
                foreach (var detalle in compraDetalles)
                {
                    // Actualizar Stock y Precio del Producto
                    var producto = await _db.Productos.FindAsync(detalle.Id_Producto);
                    if (producto != null)
                    {
                        producto.Stock += detalle.Cantidad;
                        producto.PrecioVenta = detalle.Precio;
                        producto.Costo =( detalle.Precio / 1.35m);

                        _db.Productos.Update(producto);
                        await _db.SaveChangesAsync();
                    }

                    // Crear Detalle del Movimiento
                    var movimientoDetalle = new MovimientoDet
                    {
                        Id_movimientocab = movCab_Id,
                        Id_Producto = detalle.Id_Producto,
                        Cantidad = detalle.Cantidad
                    };

                    await _db.MovimientoDets.AddAsync(movimientoDetalle);
                    await _db.SaveChangesAsync();
                }

                response.Result = _mapper.Map<CompraCab>(compraCabecera);
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
