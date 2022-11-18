using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Entities
{
    public class Sucursal : IEntity
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public IList<Inventario>? Inventario { get; set; }

    }
}
