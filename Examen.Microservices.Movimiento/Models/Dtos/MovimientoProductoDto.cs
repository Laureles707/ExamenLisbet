using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Movimiento.Models.Dtos
{
    public class MovimientoProductoDto
    {
        [Required]
        public string fecha_registro { get; set; }
        [Required]
        public string tipo_movimiento { get; set; }
        [Required]
        public int cantidad { get; set; }
     
    }
}
