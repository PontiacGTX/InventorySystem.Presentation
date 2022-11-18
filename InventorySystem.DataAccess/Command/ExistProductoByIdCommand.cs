using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class ExistProductoByIdCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public class ExistProductoByIdCommandHandler : IRequestHandler<ExistProductoByIdCommand, bool>
        {
            private AppDbContext _ctx { get; }

            public ExistProductoByIdCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<bool> Handle(ExistProductoByIdCommand request, CancellationToken cancellationToken)
            {
                return await _ctx.Productos.AnyAsync(x => x.Id == request.Id);
            }
        }
    }
}
