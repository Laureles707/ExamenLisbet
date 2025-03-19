using System.ComponentModel.DataAnnotations;

namespace Examen.Web.Models
{
    public class ProductoRequestDto
    {
  
        public string Nombre_producto { get; set; } =string.Empty;
    
        public decimal NroLote { get; set; }
    
        public decimal Costo { get; set; }
       
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
    }
}
