using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class DeleteSucursalByIdCommand:IRequest<bool>
    {
        public Guid Id { get; set; }

        public class DeleteSucursalByIdCommandHandler:IRequestHandler<DeleteSucursalByIdCommand,bool>
        {
            private AppDbContext _ctx;

            public DeleteSucursalByIdCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<bool> Handle(DeleteSucursalByIdCommand request, CancellationToken cancellationToken)
            {
                var sucursal =await _ctx.Sucursales.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (sucursal == null) return true;
                foreach(var inventario in _ctx.Inventario.Where(x=>x.SucursalId ==request.Id))
                {
                    _ctx.Inventario.Remove(inventario);
                }
                await _ctx.SaveChangesAsync();
                _ctx.Sucursales.Remove(sucursal);
                await _ctx.SaveChangesAsync();
                return !(await _ctx.Sucursales.AnyAsync(x => x.Id == request.Id));
            }
        }
    }
}
