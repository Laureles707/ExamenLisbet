using AutoMapper;
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
    [Route("api/[controller]")]
    [ApiController]
 
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepositorio _ctRepo;
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _db;
        private ResponseDto _response;

        public ProductoController(IProductoRepositorio ctRepo, IMapper mapper,ApplicationDBContext db)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
            _db = db;
            _response = new ResponseDto();
        }
        //[EnableCors("AnotherPolicy")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ResponseDto GetProductos()
        {
         

            try
            {
                IEnumerable<Producto> objList = _db.Productos.ToList();


                _response.Result = objList;

                _response.Result = _mapper.Map<IEnumerable<ProductoDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ResponseDto GetStockProducto(int IdProducto)
        //{


        //    try
        //    {
        //       // IEnumerable<Producto> objList = _db.Productos.Where(p => p.Id_Producto == IdProducto);
        //        Producto objProducto = _db.Productos.SingleOrDefault(p => p.Id_Producto == IdProducto);
        //        var totalStock = 0;


        //        if (objProducto != null)
        //        {
        //            var stockIngresoCab = (from c in _db.MovimientoCabs
        //                                   join d in _db.MovimientoDets on c.Id_MovimientoCab equals d.Id_movimientocab
        //                                   where c.Id_TipoMovimiento == 1 && d.Id_Producto == IdProducto
        //                                   select d.Cantidad).Sum();

        //            var stockSalidaCab = (from c in _db.MovimientoCabs
        //                                  join d in _db.MovimientoDets on c.Id_MovimientoCab equals d.Id_movimientocab
        //                                  where c.Id_TipoMovimiento == 2 && d.Id_Producto == IdProducto
        //                                  select d.Cantidad).Sum();

        //            totalStock = stockIngresoCab - stockSalidaCab;
        //        }

        //        objProducto.Stock = totalStock;
        //        IEnumerable<Producto> objList = new List<Producto> { objProducto };


        //        _response.Result = objList;
        //        _response.Result = _mapper.Map<IEnumerable<ProductoDto>>(objList);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}
        [HttpGet]
        [Route("GetProducto/{id}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProducto(int id)
        {
            var itemProducto = _ctRepo.GetProducto(id);

            if (itemProducto == null)
            {
                return NotFound();
            }

            var itemProductoDto = _mapper.Map<ProductoDto>(itemProducto);

            return Ok(itemProductoDto);
        }
   
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ResponseDto CrearProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            try
            {
                Producto obj = _mapper.Map<Producto>(crearProductoDto);
                _db.Productos.Add(obj);
                _db.SaveChanges();




                _response.Result = _mapper.Map<CrearProductoDto>(obj);
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
