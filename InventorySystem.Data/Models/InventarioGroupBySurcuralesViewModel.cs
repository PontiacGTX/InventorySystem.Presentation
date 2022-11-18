using InventorySystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Models
{
    public class InventarioGroupBySurcuralesViewModel
    {
       public IList<IGrouping<Sucursal,Inventario>> Inventarios { get; set; }
    }
}
