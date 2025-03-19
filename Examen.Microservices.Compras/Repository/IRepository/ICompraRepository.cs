using Examen.Microservices.Compras.Models.Dto;
using Examen.Microservices.Compras.Models.Dtos;

namespace Examen.Microservices.Compras.Repository.IRepository
{
    public interface ICompraRepository
    {
        Task<ResponseDto> CrearCompraAsync(CrearCompraDto crearCompraDto);
    }
}
