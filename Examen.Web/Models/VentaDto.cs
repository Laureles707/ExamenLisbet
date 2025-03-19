using Newtonsoft.Json;

namespace Examen.Web.Models
{
    public class VentaDto
    {
        public int Id_VentaCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal SubTotal  { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }

        public List<VentaDetDto> VentaDet { get; set; }

    }
}
