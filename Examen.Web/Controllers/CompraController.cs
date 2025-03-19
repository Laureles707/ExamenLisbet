using Examen.Web.Models;
using Examen.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Examen.Web.Controllers
{
    public class CompraController : Controller
    {
        private readonly ICompraService _compraService;
        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }
        public async Task<IActionResult> CompraIndex()
        {
            List<CompraDto>? list = new();

            ResponseDto? response = await _compraService.GetCompras();

            if (response != null && response.IsSuccess) {

                list = JsonConvert.DeserializeObject<List<CompraDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CrearCompra()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearCompra([FromBody]  CompraRequestDto request)
        {
           if(request == null)
            {
                return BadRequest(new { mensaje = "Los datos enviados son invalidos" });
            }
            try
            {
                ResponseDto? response = await _compraService.CrearCompra(request.compra);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Compra creada satisfactoriamente";
                    return Ok(new { mensaje = "Compra guardada correctamente" });
                }

            }
            catch (Exception ex) {
                return StatusCode(500, new { mensaje = "Error interno", error = ex.Message });
            }
            return View(request);
            
        }
    }
}
