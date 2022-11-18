using InventorySystem.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Request;

public class InventarioByIdRequest:IRequest<Inventario>
{
    public Guid InventarioId { get; set; }
    class InventarioByIdRequestHandler :IRequestHandler<InventarioByIdRequest,Inventario>
    {
        private AppDbContext _ctx { get; }

        public InventarioByIdRequestHandler(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Inventario> Handle(InventarioByIdRequest request, CancellationToken cancellationToken)
        {
            return await _ctx.Inventario.FirstOrDefaultAsync(x => x.Id == request.InventarioId);
        }
    }
}
public class InventarioBySucursalIdRequest: IRequest<IList<Inventario>>
{
    public Guid SucursalId { get; set; }
    public class InventarioBySucursalIdRequestHandler : IRequestHandler<InventarioBySucursalIdRequest, IList<Inventario>>
    {
        private AppDbContext _ctx { get; }

        public InventarioBySucursalIdRequestHandler(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IList<Inventario>> Handle(InventarioBySucursalIdRequest request, CancellationToken cancellationToken)
        {
            return await _ctx.Inventario.Where(x => x.SucursalId == request.SucursalId).ToListAsync();
        }
    }
}

public class InventarioByProductoIdRequest : IRequest<IList<Inventario>>
{
    public Guid ProductoId { get; set; }
    class InventarioByProductoIdRequestHandler : IRequestHandler<InventarioByProductoIdRequest, IList<Inventario>>
    {
        private AppDbContext _ctx { get; }

        public InventarioByProductoIdRequestHandler(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IList<Inventario>> Handle(InventarioByProductoIdRequest request, CancellationToken cancellationToken)
        {
            return await _ctx.Inventario.Where(x => x.ProductoId == request.ProductoId).ToListAsync();
        }
    }
}

public class InventarioGroupListRequest : IRequest<IList<IGrouping<Sucursal, Inventario>>>
{
    public Guid SucursalId { get; set; }
    public class InventarioGroupListRequestHandler:IRequestHandler<InventarioGroupListRequest, IList<IGrouping<Sucursal,Inventario>>>
    {
        private AppDbContext _ctx;

        public InventarioGroupListRequestHandler(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IList<IGrouping<Sucursal, Inventario>>> Handle(InventarioGroupListRequest request, CancellationToken cancellationToken)
        {
            
            var inventario = (await _ctx.Inventario.Where(x => x.SucursalId == ((request.SucursalId == Guid.Empty) ? x.SucursalId : request.SucursalId))
                .Include(x=>x.Sucursal)
                .Include(x=>x.Producto)
                .ToListAsync())
                .GroupBy(x => x.Sucursal)
                .ToList();
            return inventario!;
        }
    }
}

