using AutoMapper;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dtos;

namespace Examen.Microservices.Compras.Mappers
{
    public class VentaMapper : Profile
    {
        public VentaMapper() {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, CrearProductoDto>().ReverseMap();

            CreateMap<VentaCab, VentaDto>().ReverseMap();
            CreateMap<VentaCab, CrearVentaDto>().ReverseMap();
        }

   
    }
}
