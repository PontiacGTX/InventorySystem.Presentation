using InventorySystem.Data;
using InventorySystem.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class CreateSucursalCommand : IPropertyFieldHolder<Sucursal>, IRequest<Sucursal>
    {
        public Sucursal Field { get; set; }

        public class CreateSucursalCommandHandler : IRequestHandler<CreateSucursalCommand, Sucursal>
        {
            protected AppDbContext _ctx { get; }
            public CreateSucursalCommandHandler(AppDbContext context)
            {
                _ctx = context;
            }
            public async Task<Sucursal> Handle(CreateSucursalCommand command, CancellationToken cancellationToken)
            {
                var sucursal = new Sucursal();
                sucursal.Id = command.Field.Id;
                sucursal.Nombre = command.Field.Nombre;
                _ctx.Sucursales.Add(sucursal);
                await _ctx.SaveChangesAsync();
                return sucursal;
            }
        }
    }
}
