using Examen.Web.Models;
using Examen.Web.Service.IService;
using Examen.Web.Utility;

namespace Examen.Web.Service
{
    public class ProductoService: IProductoService
    {
        private readonly IBaseService _baseService;

        public ProductoService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CrearProducto(ProductoRequestDto producto)
        {
            try
            {
                return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Data = producto,
                    Url = SD.GatewayAPI + "/gateway/producto"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ResponseDto?> GetProductos()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.GatewayAPI + "/gateway/producto"
            });
        }

       
    }
}
