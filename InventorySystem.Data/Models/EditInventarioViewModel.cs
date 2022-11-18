using InventorySystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Models
{
    public class EditInventarioViewModel
    {
        public IList<Producto> Productos { get; set; }
        public IList<Sucursal> Sucursales { get; set; }
        public Inventario Inventario { get; set; }
    }
}
