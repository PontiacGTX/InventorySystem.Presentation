using InventorySystem.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class ExistProductoByExpressionCommand:IRequest<bool>
    {
        public Expression<Func<Producto,bool>> Expression { get; set; }
        class ExistProductoByExpressionCommandHandler : IRequestHandler<ExistProductoByExpressionCommand, bool>
        {
            private AppDbContext _ctx { get; }

            public ExistProductoByExpressionCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<bool> Handle(ExistProductoByExpressionCommand request, CancellationToken cancellationToken)
            {
                return await _ctx.Productos.AnyAsync(request.Expression);
            }
        }
    }
}
