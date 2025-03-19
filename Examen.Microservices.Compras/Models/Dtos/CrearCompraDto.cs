namespace Examen.Microservices.Compras.Models.Dtos
{
    public class CrearCompraDto
    {
        public DateTime FecRegistro { get; set; }
        public List<CrearCompraDetDto> compraDet { get; set; }


    }
}
