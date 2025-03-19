using System.ComponentModel.DataAnnotations;

namespace Examen.Web.Models
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
