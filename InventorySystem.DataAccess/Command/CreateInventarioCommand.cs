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
    public class CreateInventarioCommand : IPropertyFieldHolder<Inventario>, IRequest<Inventario>
    {
        public Inventario Field { get; set; }
        class CreateInventarioCommandHandler : IRequestHandler<CreateInventarioCommand, Inventario>
        {
            private AppDbContext _ctx { get; }

            public CreateInventarioCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<Inventario> Handle(CreateInventarioCommand request, CancellationToken cancellationToken)
            {
                Inventario inventarioItem = new();
                inventarioItem.Id = request.Field.Id;
                inventarioItem.ProductoId = request.Field.ProductoId;
                inventarioItem.SucursalId = request.Field.SucursalId;
                _ctx.Inventario.Add(inventarioItem);
                await _ctx.SaveChangesAsync();
                return (await _ctx.Inventario.FirstOrDefaultAsync(x=>x.Id == request.Field.Id))!;

            }
        }
    }
}
