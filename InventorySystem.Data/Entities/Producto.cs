using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Entities
{
    public  class Producto:IEntity
    {
        [Key]
        public Guid? Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string CodigoDeBarra { get; set; }

        [Range(0,double.MaxValue,ErrorMessage ="El valor minimo debe ser mayor o igual a {0}")]
        public int CantidadDisponible { get; set; }
        [Range(double.MinValue,
        double.MaxValue, ErrorMessage = "El valor minimo debe ser mayor o igual a {0} y menor a {1}")]
        public double PrecioUnitario { get; set; }
        public IList<Inventario>? Inventario { get; set; }
    }
}
