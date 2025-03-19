using System;
using AutoMapper;
using Azure.Core;
using Examen.Microservices.Compras.Data;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Compras.Repository.IRepository;
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

        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _db;
        private readonly ICompraRepository _compraRepository;

        public CompraController(IMapper mapper, ApplicationDBContext db, ICompraRepository compraRepository)
        {

            _mapper = mapper;
            _db = db;
            _compraRepository = compraRepository;
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
        public async Task<IActionResult> CrearCompra([FromBody] CrearCompraDto crearCompraDto)
        {
            var response = await _compraRepository.CrearCompraAsync(crearCompraDto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
           

        }
    }
}
