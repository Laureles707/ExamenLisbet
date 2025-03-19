using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Movimiento.Models
{
    public class MovimientoCab
    {
        [Key]
        public int Id_MovimientoCab { get; set; }
        [Required]
        public DateTime Fec_registro { get; set; }
        [Required]
        public int Id_TipoMovimiento { get; set; }
        [Required]
        public int Id_DocumentoOrigen { get; set; }
    }
}
