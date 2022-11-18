using InventorySystem.Data.Entities;
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
    
    public class SucursalService
    {
       
        private IMediator _mediator { get; }

        public SucursalService(IMediator mediator)
        {
            _mediator=mediator;
        }
        public async Task<IList<Sucursal>> GetAll()
        {
            return await _mediator.Send(new SucursalListRequest());
        }
        public async Task<Sucursal> GetSucursal(Guid guid)
        {
            return await _mediator.Send(new GetSucursalByIdQuery() { Id = guid });
        }
        public async Task<Sucursal> CreateSucursal(Sucursal sucursal)
        {
            if (sucursal.Id == Guid.Empty || sucursal.Id == null )
                sucursal.Id = Guid.NewGuid();

            return await _mediator.Send(new CreateSucursalCommand() { Field = sucursal });
        }

        public async Task<Sucursal> UpdateSucursal(Sucursal sucursal)
        {
            return await _mediator.Send(new CreateSucursalCommand() { Field = sucursal });
        }

        public async Task<bool> DeleteSucursalById(Guid SucursalId)
        {
            return await _mediator.Send(new DeleteSucursalByIdCommand() { Id = SucursalId });
        }
        public async Task<bool> ExistSucursalById(Guid id)
        {
            return await _mediator.Send(new ExistSucursalByIdCommand() { Id = id });
        }
        public async Task<bool> ExistSucursalByExpression(Expression<Func<Sucursal,bool>> expression)
        {
            return await _mediator.Send(new ExistSucursalByExpressionCommand() { Expression = expression });

        }
    }
}
