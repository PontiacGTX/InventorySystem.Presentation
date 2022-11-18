using InventorySystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Models
{
    public class CrearInventarioViewModel
    {
        public IList<Sucursal> Sucursales { get; set; }
        public IList<SelectItem<Producto>> Productos { get; set; }
    }
}
