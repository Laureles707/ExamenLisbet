using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen.Microservices.Compras.Models
{
    public class Producto
    {
        [Key]
        public int Id_Producto { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Nombre_producto { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int NroLote { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Fec_registro { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Costo { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioVenta { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Stock { get; set; }
    }
}
