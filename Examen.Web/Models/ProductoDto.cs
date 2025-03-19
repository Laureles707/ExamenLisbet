namespace Examen.Web.Models
{
    public class ProductoDto
    {
        public int Id_Producto { get; set; }
        public string Nombre_producto { get; set; }
        public int NroLote { get; set; }
        public DateTime Fec_registro { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Stock { get; set; }
    }
}
