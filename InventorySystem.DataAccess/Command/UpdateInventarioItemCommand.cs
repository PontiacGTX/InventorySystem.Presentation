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
    public class UpdateInventarioItemCommand : IPropertyFieldHolder<Inventario>, IRequest<Inventario>
    {
        public Inventario Field { get; set; }

        class UpdateInventarioItemCommandHandler : IRequestHandler<UpdateInventarioItemCommand, Inventario>
        {
            private AppDbContext _ctx;

            public UpdateInventarioItemCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<Inventario> Handle(UpdateInventarioItemCommand command, CancellationToken cancellationToken)
            {
                var inventarioItem = await _ctx.Inventario.FirstOrDefaultAsync(x => x.Id == command.Field.Id);
                if (inventarioItem is null) return default!;
                inventarioItem.SucursalId = command.Field.SucursalId;
                inventarioItem.ProductoId = command.Field.ProductoId;
                await _ctx.SaveChangesAsync();
                return (await _ctx.Inventario.FirstOrDefaultAsync(x => x.Id == command.Field.Id))!;
            }
        }
    }
}
