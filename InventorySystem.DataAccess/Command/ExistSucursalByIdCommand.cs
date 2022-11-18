using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class ExistSucursalByIdCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        class ExistSucursalByIdCommandHandler : IRequestHandler<ExistSucursalByIdCommand, bool>
        {
            private AppDbContext _ctx { get; }

            public ExistSucursalByIdCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<bool> Handle(ExistSucursalByIdCommand request, CancellationToken cancellationToken)
            {
                return await _ctx.Sucursales.AnyAsync(x=>x.Id == request.Id);
            }
        }
    }
}
