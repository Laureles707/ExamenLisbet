namespace Examen.Web.Models
{
    public class CompraDetDto
    {
        public int Id_CompraDet { get; set; } = 0;
        public int Id_CompraCab { get; set; } = 0;
        public int Id_producto { get; set; }
        public string Nombre_producto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
    }
}
