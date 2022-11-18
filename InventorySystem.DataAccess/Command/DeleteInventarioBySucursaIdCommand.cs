using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
   
    public class DeleteInventarioBySucursaIdCommand : IRequest<bool>
    {
        public Guid SucursalId { get; set; }
        public class DeleteInventarioBySucursaIdCommandHandler : IRequestHandler<DeleteInventarioBySucursaIdCommand, bool>
        {
            protected AppDbContext _ctx { get; }
            public DeleteInventarioBySucursaIdCommandHandler(AppDbContext context)
            {
                _ctx = context;
            }
            public async Task<bool> Handle(DeleteInventarioBySucursaIdCommand command, CancellationToken cancellationToken)
            {

                foreach (var item in _ctx.Inventario.Where(a => a.SucursalId == command.SucursalId))
                {
                    _ctx.Inventario.Remove(item);
                }

                await _ctx.SaveChangesAsync();
                return !_ctx.Inventario.Any(x => x.SucursalId == command.SucursalId);
            }
        }
    }
}
