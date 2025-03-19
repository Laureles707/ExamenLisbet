using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen.Microservices.Compras.Models
{
    public class MovimientoCab
    {
        [Key]
        public int Id_MovimientoCab { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Fec_registro { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Id_TipoMovimiento { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Id_DocumentoOrigen { get; set; }
    }
}
