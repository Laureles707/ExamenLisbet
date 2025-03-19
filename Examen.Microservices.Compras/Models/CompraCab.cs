using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen.Microservices.Compras.Models
{
    public class CompraCab
    {
        [Key]
        public int Id_CompraCab { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime FecRegistro { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SubTotal { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Igv { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
    }
}
