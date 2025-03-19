namespace Examen.Microservices.Compras.Models.Dtos
{
    public class CrearVentaDto
    {
        public DateTime FecRegistro { get; set; }
        public List<CrearVentaDetDto> ventaDet { get; set; }


    }
}
