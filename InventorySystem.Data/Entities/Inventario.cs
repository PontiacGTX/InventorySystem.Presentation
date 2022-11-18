using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Entities;

public class Inventario:IEntity
{
    [Key]
    public Guid? Id { get; set; }

    public  Guid ProductoId { get; set; }
    [ForeignKey("ProductoId")]
    public Producto? Producto { get; set; }

    [ForeignKey("SucursalId")]
    public Sucursal? Sucursal { get; set; }
    public Guid SucursalId { get; set; }

}
