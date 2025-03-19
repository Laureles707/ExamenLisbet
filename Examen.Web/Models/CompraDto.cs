using Newtonsoft.Json;

namespace Examen.Web.Models
{
    public class CompraDto
    {
        public int Id_CompraCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal SubTotal  { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }

        public List<CompraDetDto> CompraDet { get; set; }

    }
}
