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
    public class DeleteProductoByIdCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductoByIdCommand, bool>
        {
            protected AppDbContext _ctx { get; }
            public DeleteProductByIdCommandHandler(AppDbContext context)
            {
                _ctx = context;
            }
            public async Task<bool> Handle(DeleteProductoByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _ctx.Productos.FirstOrDefaultAsync(a => a.Id == command.Id);
                if (product == null) return true;
                foreach (var item in _ctx.Inventario.Where(x=>x.ProductoId == command.Id))
                {
                    _ctx.Inventario.Remove(item);
                }
                _ctx.Productos.Remove(product);
                await _ctx.SaveChangesAsync();
                return !_ctx.Productos.Any(x=>x.Id== command.Id);
            }
        }
    }
}
