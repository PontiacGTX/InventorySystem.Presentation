using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class ExistInventarioByIdCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public class ExistInventarioByIdCommandHandler: IRequestHandler<ExistInventarioByIdCommand,bool>
        {
            private AppDbContext _ctx { get; }

            public ExistInventarioByIdCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<bool> Handle(ExistInventarioByIdCommand request, CancellationToken cancellationToken)
            {
                return await _ctx.Inventario.AnyAsync(x => x.Id == request.Id);
            }
        }
    }
}
