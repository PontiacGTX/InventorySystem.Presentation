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
    public  class ExistSucursalByExpressionCommand : IRequest<bool>
    {
        public Expression<Func<Sucursal,bool>> Expression { get; set; }
        class ExistSucursalByExpressionCommandHandler : IRequestHandler<ExistSucursalByExpressionCommand, bool>
        {
            private AppDbContext _ctx { get; }

            public ExistSucursalByExpressionCommandHandler(AppDbContext ctx)
            {
                _ctx = ctx;
            }
            public async Task<bool> Handle(ExistSucursalByExpressionCommand request, CancellationToken cancellationToken)
            {
                return await _ctx.Sucursales.AnyAsync(request.Expression);
            }
        }
    }
}
