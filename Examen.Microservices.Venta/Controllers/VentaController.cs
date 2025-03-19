using System;
using AutoMapper;
using Azure.Core;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Venta.Data;
using Examen.Microservices.Venta.Repository.IRepository;
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

  
        private readonly IVentaRepository _ventaRepository;

        public VentaController(  IVentaRepository ventaRepository)
        {

            _ventaRepository = ventaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVentas()
        {

            var response = await _ventaRepository.GetVentasAsync();
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
       
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] CrearVentaDto crearVentaDto)
        {
            var response = await _ventaRepository.CrearVentaAsync(crearVentaDto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
       
        }

    }
}
