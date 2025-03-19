using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Compras.Models.Dtos
{
    public class CrearProductoDto
    {
        [Required]
        public string Nombre_producto { get; set; }
        [Required]
        public decimal NroLote { get; set; }
        [Required]
        public decimal Costo { get; set; }
        [Required]
        public decimal PrecioVenta { get; set; }
        [Required]
        public int Stock { get; set; }

    }
}
