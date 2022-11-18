using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class DeleteInventarioByProductoIdCommand : IRequest<bool>
    {
        public Guid ProductoId { get; set; }
        public class DeleteInventarioByProductoIdCommandHandler : IRequestHandler<DeleteInventarioByProductoIdCommand, bool>
        {
            protected AppDbContext _ctx { get; }
            public DeleteInventarioByProductoIdCommandHandler(AppDbContext context)
            {
                _ctx = context;
            }
            public async Task<bool> Handle(DeleteInventarioByProductoIdCommand command, CancellationToken cancellationToken)
            {
                
                foreach (var item in _ctx.Inventario.Where(a => a.ProductoId == command.ProductoId))
                {
                    _ctx.Inventario.Remove(item);
                }
                
                await _ctx.SaveChangesAsync();
                return !_ctx.Inventario.Any(x => x.ProductoId == command.ProductoId);
            }
        }
    }
}
