using Examen.Web.Models;
using Examen.Web.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Examen.Web.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly IMovimientoService _movService;
        private readonly IProductoService _prodService;
        public MovimientoController(IMovimientoService movService, IProductoService prodService)
        {
            _movService = movService;
            _prodService = prodService;
        }
        public async Task<IActionResult> MovimientoIndex()
        {
            List<ProductoDto>? list = new();

            ResponseDto? response = await _prodService.GetProductos();

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<ProductoDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> getMovimientoProducto(int id)
        {
            List<MovimientoProductoDto>? list = new();

            ResponseDto? response = await _movService.GetMovimientoProducto(id);

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<MovimientoProductoDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return Ok(list);
        }

    }
}
