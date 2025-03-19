using AutoMapper;
using Examen.Microservices.Compras.Data;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Compras.Sevice.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Examen.Microservices.Compras.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
 
    public class ProductoController : ControllerBase
    {

        private IMapper _mapper;
        private ResponseDto _response;
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IProductoRepository productoRepository, IMapper mapper)
        {


            _productoRepository = productoRepository;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductos()
        {


            try
            {
                var productos = await _productoRepository.ObtenerProductoAsync();
                _response.Result = _mapper.Map<IEnumerable<ProductoDto>>(productos);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }
       
   
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            try
            {
                var producto = await _productoRepository.CrearProductoAsync(crearProductoDto);
                _response.Result = _mapper.Map<CrearProductoDto>(producto);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }
    }
}
