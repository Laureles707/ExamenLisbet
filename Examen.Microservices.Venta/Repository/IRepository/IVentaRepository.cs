using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;

namespace Examen.Microservices.Venta.Repository.IRepository
{
    public interface IVentaRepository
    {
        Task<ResponseDto> GetVentasAsync();
        Task<ResponseDto> CrearVentaAsync(CrearVentaDto crearVentaDto);
    }
}
