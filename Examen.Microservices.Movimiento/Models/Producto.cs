using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Movimiento.Models
{
    public class Producto
    {
        [Key]
        public int Id_Producto { get; set; }
        [Required]
        public string Nombre_producto { get; set; }
        [Required]
        public int NroLote { get; set; }
        [Required]
        public DateTime Fec_registro { get; set; }
        [Required]
        public decimal Costo { get; set; }
        [Required]
        public decimal PrecioVenta { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
