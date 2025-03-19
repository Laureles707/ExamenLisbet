using AutoMapper;
using Examen.Microservices.Movimiento.Models;
using Examen.Microservices.Movimiento.Models.Dtos;

namespace Examen.Microservices.Movimiento.Mappers
{
    public class MovimientoMapper : Profile
    {
        public MovimientoMapper() {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, CrearProductoDto>().ReverseMap();

            CreateMap<CompraCab, CompraDto>().ReverseMap();
            CreateMap<CompraCab, CrearCompraDto>().ReverseMap();
        }

   
    }
}
