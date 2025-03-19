using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Compras.Models
{
    public class MovimientoDet
    {
        [Key]
        public int Id_MovimientoDet { get; set; }
        [Required]
        public int Id_movimientocab { get; set; }
        [Required]
        public int Id_Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
    }
}
