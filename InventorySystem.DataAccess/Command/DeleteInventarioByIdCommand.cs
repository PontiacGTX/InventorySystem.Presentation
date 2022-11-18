using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class DeleteInventarioByIdCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public class DeleteInventarioByIdCommandHandler : IRequestHandler<DeleteInventarioByIdCommand, bool>
        {
            private AppDbContext _ctx;

            public DeleteInventarioByIdCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<bool> Handle(DeleteInventarioByIdCommand request, CancellationToken cancellationToken)
            {
                var inventario = await _ctx.Inventario.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (inventario is null) return true;
                _ctx.Inventario.Remove(inventario);
                await _ctx.SaveChangesAsync();
                return !(await _ctx.Inventario.AnyAsync(x=>x.Id ==request.Id));
            }
        }
    }
}
