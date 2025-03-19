namespace Examen.Web.Models
{
    public class VentaDetDto
    {
        public int Id_VentaDet { get; set; } = 0;
        public int Id_VentaCab { get; set; } = 0;
        public int Id_producto { get; set; }
        public string Nombre_producto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
    }
}
