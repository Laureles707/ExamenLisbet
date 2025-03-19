namespace Examen.Microservices.Movimiento.Models.Dtos
{
    public class CrearCompraDto
    {
        public DateTime FecRegistro { get; set; }
        public List<CrearCompraDetDto> compraDet { get; set; }


    }
}
