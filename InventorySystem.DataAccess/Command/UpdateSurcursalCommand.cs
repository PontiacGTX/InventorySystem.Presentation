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
    public class UpdateSurcursalCommand : IPropertyFieldHolder<Sucursal>, IRequest<Sucursal>
    {
        public Sucursal Field { get; set; }
        class UpdateSurcursalCommandHandler : IRequestHandler<UpdateSurcursalCommand, Sucursal>
        {
            private AppDbContext _ctx { get; }

            public UpdateSurcursalCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<Sucursal> Handle(UpdateSurcursalCommand command, CancellationToken cancellationToken)
            {
                var sucursal =await _ctx.Sucursales.FirstOrDefaultAsync(x => x.Id == command.Field.Id);
                if (sucursal is null) return default!;
                sucursal.Nombre = command.Field.Nombre;
                //_ctx.Entry(sucursal).CurrentValues.SetValues(command.Field);
                await  _ctx.SaveChangesAsync();
                return (await _ctx.Sucursales.FirstOrDefaultAsync(x => x.Id == command.Field.Id))!;

            }
        }
    }
}
