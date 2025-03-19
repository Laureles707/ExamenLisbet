using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Movimiento.Models.Dtos
{
    public class CompraDto
    {

        public int Id_CompraCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
    }
}
