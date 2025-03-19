using System;
using AutoMapper;
using Azure.Core;
using Examen.Microservices.Movimiento.Data;
using Examen.Microservices.Movimiento.Models;
using Examen.Microservices.Movimiento.Models.Dto;
using Examen.Microservices.Movimiento.Models.Dtos;
using Examen.Microservices.Movimiento.Repository.IRepository;
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

    
        private readonly IMovimientoRepository _movRepository;

        public MovimientoController(IMapper mapper, IMovimientoRepository movRepository)
        {
       
        
            _movRepository = movRepository;
        }


        [HttpGet]
        [Route("GetKardex/{Idproducto:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetKardex( int Idproducto )
        {

            var response = await _movRepository.GetKardexAsync(Idproducto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }



    }
}
