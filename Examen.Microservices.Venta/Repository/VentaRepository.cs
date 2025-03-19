using AutoMapper;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Venta.Data;
using Examen.Microservices.Venta.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Examen.Microservices.Venta.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public VentaRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        

        public async Task<ResponseDto> GetVentasAsync()
        {
            var response = new ResponseDto();
            try
            {
                var objList = await _db.VentaCabs.ToListAsync();
                response.Result = _mapper.Map<IEnumerable<VentaCab>>(objList);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> CrearVentaAsync(CrearVentaDto crearVentaDto)
        {
            var response = new ResponseDto();
            try
            {
                // Crear Cabecera de la Venta
                var ventaCabecera = new VentaCab
                {
                    FecRegistro = DateTime.Now,
                    SubTotal = crearVentaDto.ventaDet.Sum(d => d.Precio * d.Cantidad),
                    Igv = crearVentaDto.ventaDet.Sum(d => d.Precio * d.Cantidad) * 0.18m,
                    Total = crearVentaDto.ventaDet.Sum(d => d.Precio * d.Cantidad) * 1.18m
                };

                await _db.VentaCabs.AddAsync(ventaCabecera);
                await _db.SaveChangesAsync();
                var id_cab = ventaCabecera.Id_VentaCab;

                // Crear Detalles de la Venta
                var ventaDetalles = crearVentaDto.ventaDet.Select(d => new VentaDet
                {
                    Id_Producto = d.Id_producto,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                    Sub_Total = d.Precio * d.Cantidad,
                    Igv = (d.Precio * d.Cantidad) * 0.18m,
                    Total = (d.Precio * d.Cantidad) * 1.18m,
                    Id_VentaCab = id_cab
                }).ToList();

                await _db.VentaDets.AddRangeAsync(ventaDetalles);
                await _db.SaveChangesAsync();

                // Crear Cabecera del Movimiento
                var movimientoCabecera = new MovimientoCab
                {
                    Fec_registro = DateTime.Now,
                    Id_TipoMovimiento = 2,
                    Id_DocumentoOrigen = id_cab
                };

                await _db.MovimientoCabs.AddAsync(movimientoCabecera);
                await _db.SaveChangesAsync();
                var movCab_Id = movimientoCabecera.Id_MovimientoCab;

                // Procesar cada Detalle
                foreach (var detalle in ventaDetalles)
                {
                    // Actualizar Stock del Producto
                    var producto = await _db.Productos.FindAsync(detalle.Id_Producto);
                    if (producto != null)
                    {
                        producto.Stock -= detalle.Cantidad;
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

                response.Result = _mapper.Map<VentaCab>(ventaCabecera);
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
