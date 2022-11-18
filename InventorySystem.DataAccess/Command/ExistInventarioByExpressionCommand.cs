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
    public class ExistInventarioByExpressionCommand:IRequest<bool>
    {
        public Expression<Func<Inventario,bool>> Expression { get; set; }
        class ExistInventarioByExpressionCommandHandler : IRequestHandler<ExistInventarioByExpressionCommand, bool>
        {
            private AppDbContext _ctx;
            public ExistInventarioByExpressionCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<bool> Handle(ExistInventarioByExpressionCommand request, CancellationToken cancellationToken)
            {
                return await _ctx.Inventario.AnyAsync(request.Expression);
            }
        }
    }
}
