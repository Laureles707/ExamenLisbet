using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen.Microservices.Compras.Models
{
    public class MovimientoDet
    {
        [Key]
        public int Id_MovimientoDet { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Id_movimientocab { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Id_Producto { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Cantidad { get; set; }
    }
}
