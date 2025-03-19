using AutoMapper;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dtos;

namespace Examen.Microservices.Compras.Mappers
{
    public class ComprasMapper : Profile
    {
        public ComprasMapper() {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, CrearProductoDto>().ReverseMap();

            CreateMap<CompraCab, CompraDto>().ReverseMap();
            CreateMap<CompraCab, CrearCompraDto>().ReverseMap();
        }

   
    }
}
