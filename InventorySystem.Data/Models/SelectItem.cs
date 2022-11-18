using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Models
{
    public class SelectItem<T> where T : class
    {
        public T Value { get; set; }
        public bool Enable { get; set; }
    }
}
