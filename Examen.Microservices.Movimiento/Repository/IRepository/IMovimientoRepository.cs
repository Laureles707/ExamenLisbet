using Examen.Microservices.Movimiento.Models.Dto;

namespace Examen.Microservices.Movimiento.Repository.IRepository
{
    public interface IMovimientoRepository
    {
        Task<ResponseDto> GetKardexAsync(int idProducto);
    }
}
