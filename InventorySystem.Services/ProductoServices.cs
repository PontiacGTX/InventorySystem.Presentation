using InventorySystem.Data.Entities;
using InventorySystem.Data.Models;
using InventorySystem.DataAccess.Command;
using InventorySystem.DataAccess.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Services
{
    public  class ProductoServices
    {
        public IMediator _mediator { get; set; }
        public ProductoServices(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IList<SelectItem<Producto>>> GetProductListNotRegisteredBySucuralId(Guid? sucursalId)
        {
            return await _mediator.Send(new ProductosSelectListRequest() { SucursalId = sucursalId });
        }
        public async Task<Producto> GetProducto(Guid id)
        {
            return await _mediator.Send(new GetProductoByIdQuery() { Id= id});
        }
        public async Task<IList<Producto>> GetAll()
        {
            return await _mediator.Send(new ProductosListRequest());
        }
        public async Task<Producto> CreateProducto(Producto producto)
        {

            if (producto.Id == Guid.Empty || producto.Id == null)
                producto.Id = Guid.NewGuid();

            producto = await _mediator.Send(new CreateProductoCommand() { Field = producto });
            return producto;
        }
        public async Task<bool> DeleteProducto(Guid id)
        {
            return await _mediator.Send(new DeleteProductoByIdCommand() { Id = id });
        }
        public async Task<bool> ExistProducto(Expression<Func<Producto,bool>> expression)
        {
            return await _mediator.Send(new ExistProductoByExpressionCommand() { Expression = expression });
        }
        public async Task<bool> ExistProducto(Guid id)
        {
            return await _mediator.Send(new ExistProductoByIdCommand() { Id= id });
        }
        public async Task<Producto> UpdateProducto(Producto producto)
        {
            return await _mediator.Send(new UpdateProductoCommmand() { Field = producto });
        }
    }
}
