using Examen.Web.Models;
using Examen.Web.Service.IService;
using Examen.Web.Utility;

namespace Examen.Web.Service
{
    public class CompraService : ICompraService
    {
        private readonly IBaseService _baseService;

        public CompraService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CrearCompra(CompraDto compra)
        {
            try
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Data = compra,
                    Url = SD.GatewayAPI + "/gateway/compra"
                });
            }
            catch (Exception ex) {
                throw ex;
            }
          
        }

        public async Task<ResponseDto?> GetCompras()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.GatewayAPI + "/gateway/compra"
            });
        }
    }
}
