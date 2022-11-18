using InventorySystem.Data;
using InventorySystem.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class UpdateProductoCommmand : IPropertyFieldHolder<Producto>,IRequest<Producto>
    {
        public Producto Field { get; set; }

        class UpdateProductoCommmandHandler : IRequestHandler<UpdateProductoCommmand, Producto>
        {
            private AppDbContext _ctx;

            public UpdateProductoCommmandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<Producto> Handle(UpdateProductoCommmand command, CancellationToken cancellationToken)
            {
                var producto =await _ctx.Productos.FirstOrDefaultAsync(x => x.Id == command.Field.Id);
                if (producto is null) return null;
                producto.PrecioUnitario = command.Field.PrecioUnitario;
                producto.Nombre = command.Field.Nombre;
                producto.CodigoDeBarra = command.Field.CodigoDeBarra;
                producto.CantidadDisponible = command.Field.CantidadDisponible;
                // _ctx.Entry(producto).CurrentValues.SetValues(command.Field);
                await _ctx.SaveChangesAsync();
                return (await _ctx.Productos.FirstOrDefaultAsync(x => x.Id == producto.Id))!;
            }
        }
    }
}
