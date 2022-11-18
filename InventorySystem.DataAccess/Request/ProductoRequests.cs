using InventorySystem.Data.Entities;
using InventorySystem.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Request;

public class ProductosSelectListRequest: IRequest<IList<SelectItem<Producto>>>
{
    public Guid? SucursalId { get; set; }
    public class ProductosSelectListRequestHandler : IRequestHandler<ProductosSelectListRequest, IList<SelectItem<Producto>>>
    {
        protected AppDbContext _ctx { get; }
        public ProductosSelectListRequestHandler(AppDbContext context)
        {
            _ctx = context;
        }

        public async Task<IList<SelectItem<Producto>>> Handle(ProductosSelectListRequest request, CancellationToken cancellationToken)
        {

            var items =_ctx.Inventario.Where(x => x.SucursalId == request.SucursalId).Select(x=>x.Producto).ToDictionary(x=>x.Id);
            var productos = await _ctx.Productos.Select(x => new SelectItem<Producto>
            {
                 Enable = !items.ContainsKey(x.Id),
                 Value = x
            }).ToListAsync();
            return productos;
        }
    }


}
public class ProductosListRequest: IRequest<IList<Producto>>
{
    public class GetAllProductosQueryHandler : IRequestHandler<ProductosListRequest, IList<Producto>>
    {
        protected AppDbContext _ctx { get; }
        public GetAllProductosQueryHandler(AppDbContext context)
        {
            _ctx = context;
        }

        async Task<IList<Producto>> IRequestHandler<ProductosListRequest, IList<Producto>>.Handle(ProductosListRequest request, CancellationToken cancellationToken)
        {
            return await _ctx.Productos.ToListAsync();
            
        }
    }

   
} 
public class GetProductoByIdQuery : IRequest<Producto>
{
        public Guid Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, Producto>
        {
            protected AppDbContext _ctx { get; }
            public GetProductByIdQueryHandler(AppDbContext context)
            {
                _ctx = context;
            }
            public async Task<Producto> Handle(GetProductoByIdQuery query, CancellationToken cancellationToken)
            {
               return (await _ctx.Productos.FirstOrDefaultAsync(a => a.Id == query.Id))!;
            }
        }
}
