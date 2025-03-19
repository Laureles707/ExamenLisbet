using Examen.Web.Models;
using Examen.Web.Service;
using Examen.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Examen.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
;
        }
        public async Task<IActionResult> GetProductos()
        {
            List<ProductoDto>? list = new();

            ResponseDto? response = await  _productoService.GetProductos();

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<ProductoDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return Ok(list);
        }

        public async Task<IActionResult> listarProducto()
        {
            List<ProductoDto>? list = new();

            ResponseDto? response = await _productoService.GetProductos();

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<ProductoDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoRequestDto producto)
        {
            ResponseDto? response = new();
            if (producto == null)
            {
                return BadRequest(new { mensaje = "Los datos enviados son inválidos" });
            }
            try
            {
                response = await _productoService.CrearProducto(producto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Producto creada satisfactoriamente";
                    return Ok(new { mensaje = "Producto guardado correctamente" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno", error = ex.Message });
            }
            return Ok(response);

        }
    }
}
