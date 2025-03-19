using System;
using AutoMapper;
using Azure.Core;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Venta.Data;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Examen.Microservices.Venta.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
  
    public class VentaController : ControllerBase
    {

        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _db;


        public VentaController(IMapper mapper, ApplicationDBContext db)
        {
       
            _mapper = mapper;
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ResponseDto GetVentas()
        {
            

            try
            {
                IEnumerable<VentaCab> objList = _db.VentaCabs.ToList();
                _response.Result = _mapper.Map<IEnumerable<VentaCab>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> CrearVenta([FromBody] CrearVentaDto crearVentaDto)
        {
            
            try
            {
                var ventaCabecera = new VentaCab
                {
                    FecRegistro = DateTime.Now,
                    SubTotal = crearVentaDto.ventaDet.Sum(d => d.Precio * d.Cantidad),
                    Igv = crearVentaDto.ventaDet.Sum(d => d.Precio * d.Cantidad) * 0.18m,
                    Total = crearVentaDto.ventaDet.Sum(d => d.Precio * d.Cantidad) * 1.18m

                };

                var ventaDetalles = crearVentaDto.ventaDet.Select(d => new VentaDet
                {
                    Id_Producto = d.Id_producto,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                    Sub_Total = d.Precio * d.Cantidad,
                    Igv = (d.Precio * d.Cantidad) * 0.18m,
                    Total = (d.Precio * d.Cantidad) * 1.18m
                }).ToList();

                await _db.VentaCabs.AddAsync(ventaCabecera);
                await _db.SaveChangesAsync();

                var id_cab = ventaCabecera.Id_VentaCab;

                var movimientoCabecera = new MovimientoCab
                {
                    Fec_registro = DateTime.Now,
                    Id_TipoMovimiento = 2,
                    Id_DocumentoOrigen = id_cab,

                };
               await _db.MovimientoCabs.AddAsync(movimientoCabecera);
                await _db.SaveChangesAsync();

                var movCab_Id = movimientoCabecera.Id_MovimientoCab;

                foreach (var detalle in ventaDetalles)
                {
                    detalle.Id_VentaCab = id_cab;
                    await _db.VentaDets.AddAsync(detalle);
                    await _db.SaveChangesAsync();

                    var producto = _db.Productos.FindAsync(detalle.Id_Producto).Result;


                    producto.Stock -= detalle.Cantidad;

                    _db.Productos.Update(producto);
                    await _db.SaveChangesAsync();

                    var movimientoDetalle = new MovimientoDet
                    {
                        Id_movimientocab = movCab_Id,
                        Id_Producto = detalle.Id_Producto,
                        Cantidad = detalle.Cantidad,

                    };
                   await _db.MovimientoDets.AddAsync(movimientoDetalle);
                   await _db.SaveChangesAsync();

                }

                _response.Result = _mapper.Map<VentaCab>(ventaCabecera);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return  _response;
        }

    }
}
