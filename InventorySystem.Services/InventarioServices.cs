using InventorySystem.Data.Entities;
using InventorySystem.Data.Models;
using InventorySystem.DataAccess.Command;
using InventorySystem.DataAccess.Request;
using MediatR;
using System.Linq.Expressions;

namespace InventorySystem.Services
{
    public class InventarioServices
    {
        protected IMediator _mediator { get; }

        public InventarioServices(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<IList<Inventario>>  GetAllBySucursalId(Guid sucursalId)
        {
            return await _mediator.Send(new InventarioBySucursalIdRequest() { SucursalId = sucursalId });
        }

        public async Task<IList<IGrouping<Sucursal,Inventario>>> GetInventarioListGroupBySucursal(Guid sucursalId)
        {
            return await _mediator.Send(new InventarioGroupListRequest { SucursalId =sucursalId });
        }
        public async Task<Inventario> GetInventarioById(Guid inventarioId)
        {
            return await _mediator.Send(new InventarioByIdRequest() { InventarioId = inventarioId });
        }
        public async Task<IList<Inventario>> GetAllByProductoId(Guid productoId)
        {
            return await _mediator.Send(new InventarioByProductoIdRequest() { ProductoId = productoId });
        }
        public async Task<Inventario> CreateInventario(Inventario inventario)
        {
            if (inventario.Id == Guid.Empty || inventario.Id ==null)
                inventario.Id = Guid.NewGuid();

            return await _mediator.Send(new CreateInventarioCommand() { Field = inventario });
        }
        public async Task<bool> DeleteInventarioById(Guid id)
        {
            return await _mediator.Send(new DeleteInventarioByIdCommand { Id = id });
        }
        public async Task<bool> DeleteInventarioByProductoId(Guid productoId)
        {
            return await _mediator.Send(new DeleteInventarioByProductoIdCommand() { ProductoId = productoId });
        }
        public async Task<bool> DeleteInventarioBySucursalId(Guid sucursalId)
        {
            return await _mediator.Send(new DeleteInventarioBySucursaIdCommand() { SucursalId = sucursalId });
        }
        public async Task<Inventario> UpdateInventorio(Inventario inventario)
        {
            return await _mediator.Send(new UpdateInventarioItemCommand() { Field = inventario });
        }
        public async Task<bool> ExistInventarioByExpression(Expression<Func<Inventario,bool>> expression)
        {
            return await _mediator.Send(new ExistInventarioByExpressionCommand() { Expression = expression });  
        }
    }
}