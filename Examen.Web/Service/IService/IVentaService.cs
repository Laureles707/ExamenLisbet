using Examen.Web.Models;

namespace Examen.Web.Service.IService
{
    public interface IVentaService
    {
        Task<ResponseDto?> GetVentas();
        Task<ResponseDto?> CrearVenta(VentaDto compra);
    }
}
