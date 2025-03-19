using Examen.Web.Models;
using Examen.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Examen.Web.Controllers
{
    public class VentaController : Controller
    {
        private readonly IVentaService _ventaService;
        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }
        public async Task<IActionResult> VentaIndex()
        {
            List<VentaDto>? list = new();

            ResponseDto? response = await _ventaService.GetVentas();

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<VentaDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CrearVenta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] VentaRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { mensaje = "Los datos enviados son invalidos" });
            }
            try
            {
                ResponseDto? response = await _ventaService.CrearVenta(request.venta);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Venta creada satisfactoriamente";
                    return Ok(new { mensaje = "Venta guardada correctamente" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno", error = ex.Message });
            }
            return View(request);

        }
    }
}
