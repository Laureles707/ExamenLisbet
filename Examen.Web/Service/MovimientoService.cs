using Examen.Web.Models;
using Examen.Web.Service.IService;
using Examen.Web.Utility;

namespace Examen.Web.Service
{
    public class MovimientoService: IMovimientoService
    {
        private readonly IBaseService _baseService;

        public MovimientoService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        

        public async Task<ResponseDto?> GetMovimientoProducto(int idProducto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
  
                Url = SD.GatewayAPI + "/gateway/movimiento/" + idProducto
            });
        }

       
    }
}
