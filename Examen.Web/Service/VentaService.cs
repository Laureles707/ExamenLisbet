using Examen.Web.Models;
using Examen.Web.Service.IService;
using Examen.Web.Utility;

namespace Examen.Web.Service
{
    public class VentaService : IVentaService
    {
        private readonly IBaseService _baseService;

        public VentaService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CrearVenta(VentaDto compra)
        {
            try
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Data = compra,
                    Url = SD.GatewayAPI + "/gateway/venta"
                });
            }
            catch (Exception ex) {
                throw ex;
            }
          
        }

        public async Task<ResponseDto?> GetVentas()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.GatewayAPI + "/gateway/venta"
            });
        }
    }
}
