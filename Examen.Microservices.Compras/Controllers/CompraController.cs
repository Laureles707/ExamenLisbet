using System;
using AutoMapper;
using Azure.Core;
using Examen.Microservices.Compras.Data;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Compras.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Examen.Microservices.Compras.Controllers
{
    [Authorize] 
    [Route("api/compra")]
    [ApiController]
  
    public class CompraController : ControllerBase
    {
        private readonly ICompraRepositorio _ctRepo;
        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _db;


        public CompraController(ICompraRepositorio ctRepo, IMapper mapper, ApplicationDBContext db)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ResponseDto GetCompras()
        {
            

            try
            {
                IEnumerable<CompraCab> objList = _db.CompraCabs.ToList();
                _response.Result = _mapper.Map<IEnumerable<CompraDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> CrearCompra([FromBody] CrearCompraDto crearCompraDto)
        {
            
            try
            {
                var compraCabecera = new CompraCab
                {
                    FecRegistro = DateTime.Now,
                    SubTotal = crearCompraDto.compraDet.Sum(d => d.Precio * d.Cantidad),
                    Igv = crearCompraDto.compraDet.Sum(d => d.Precio * d.Cantidad) * 0.18m,
                    Total = crearCompraDto.compraDet.Sum(d => d.Precio * d.Cantidad) * 1.18m

                };

                var compraDetalles = crearCompraDto.compraDet.Select(d => new CompraDet
                {
                    Id_Producto = d.Id_producto,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                    Sub_Total = d.Precio * d.Cantidad,
                    Igv = (d.Precio * d.Cantidad) * 0.18m,
                    Total = (d.Precio * d.Cantidad) * 1.18m
                }).ToList();

                await _db.CompraCabs.AddAsync(compraCabecera);
                await _db.SaveChangesAsync();

                var id_cab = compraCabecera.Id_CompraCab;

                var movimientoCabecera = new MovimientoCab
                {
                    Fec_registro = DateTime.Now,
                    Id_TipoMovimiento = 1,
                    Id_DocumentoOrigen = id_cab,

                };
               await _db.MovimientoCabs.AddAsync(movimientoCabecera);
                await _db.SaveChangesAsync();

                var movCab_Id = movimientoCabecera.Id_MovimientoCab;

                foreach (var detalle in compraDetalles)
                {
                    detalle.Id_CompraCab = id_cab;
                    await _db.CompraDets.AddAsync(detalle);
                    await _db.SaveChangesAsync();

                    var producto = _db.Productos.FindAsync(detalle.Id_Producto).Result;

                    producto.Stock += detalle.Cantidad;
                    producto.PrecioVenta = detalle.Precio;
                    producto.Costo = (detalle.Precio - (detalle.Precio * 0.35m));

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

                _response.Result = _mapper.Map<CompraCab>(compraCabecera);
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
