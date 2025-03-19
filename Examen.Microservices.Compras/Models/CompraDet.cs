using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Compras.Models
{
    public class CompraDet
    {
        [Key]
        public int Id_CompraDet { get; set; }
        [Required]
        public int Id_CompraCab { get; set; }
        [Required]
        public int Id_Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public decimal Sub_Total { get; set; }
        [Required]
        public decimal Igv { get; set; }
        [Required]
        public decimal Total { get; set; }
    }
}
