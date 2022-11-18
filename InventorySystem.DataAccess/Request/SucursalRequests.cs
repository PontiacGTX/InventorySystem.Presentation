using InventorySystem.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Request
{
    public class SucursalListRequest : IRequest<IList<Sucursal>>
    {
        public class GetAllSucursalesQueryHandler : IRequestHandler<SucursalListRequest, IList<Sucursal>>
        {
            protected AppDbContext _ctx { get; }
            public GetAllSucursalesQueryHandler(AppDbContext context)
            {
                _ctx = context;
            }

            async Task<IList<Sucursal>> IRequestHandler<SucursalListRequest, IList<Sucursal>>.Handle(SucursalListRequest request, CancellationToken cancellationToken)
            {
                return await _ctx.Sucursales.ToListAsync();
            }
        }

    }

    public class GetSucursalByIdQuery : IRequest<Sucursal>
    {
        public Guid Id { get; set; }
        public class GetSucursalByIdQueryHandler : IRequestHandler<GetSucursalByIdQuery, Sucursal>
        {
            protected AppDbContext _ctx { get; }
            public GetSucursalByIdQueryHandler(AppDbContext context)
            {
                _ctx = context;
            }
            public async Task<Sucursal> Handle(GetSucursalByIdQuery query, CancellationToken cancellationToken)
            {
                return (await _ctx.Sucursales.FirstOrDefaultAsync(a => a.Id == query.Id))!;
            }
        }
    }

}
