using System;
using AutoMapper;
using Azure.Core;
using Examen.Microservices.Movimiento.Data;
using Examen.Microservices.Movimiento.Models;
using Examen.Microservices.Movimiento.Models.Dto;
using Examen.Microservices.Movimiento.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Examen.Microservices.Movimiento.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
  
    public class MovimientoController : ControllerBase
    {

        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _db;


        public MovimientoController(IMapper mapper, ApplicationDBContext db)
        {
       
            _mapper = mapper;
            _db = db;
            _response = new ResponseDto();
        }


        [HttpGet]
        [Route("GetKardex/{Idproducto:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ResponseDto GetKardex( int Idproducto )
        {
            

            try
            {
               
                var objListMov = (
                               from c in _db.MovimientoCabs
                               join d in _db.MovimientoDets on c.Id_MovimientoCab equals d.Id_movimientocab into detalles
                               from mc in detalles.DefaultIfEmpty() // Left Join 
                               where mc.Id_Producto == Idproducto
                               select new MovimientoProductoDto
                               {
                                   fecha_registro = c.Fec_registro.ToString("dd/mm/yyyy hh:mm:ss"),
                                   tipo_movimiento =(c.Id_TipoMovimiento == 1)?"Entrada":"Salida",
                                   cantidad = mc.Cantidad
                               }).ToList();

                IEnumerable<MovimientoProductoDto> objList = objListMov.ToList();
                _response.Result = _mapper.Map<IEnumerable<MovimientoProductoDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }



    }
}
