using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen.Microservices.Compras.Models
{
    public class CompraDet
    {
        [Key]
        public int Id_CompraDet { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Id_CompraCab { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Id_Producto { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Cantidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Sub_Total { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Igv { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
    }
}
