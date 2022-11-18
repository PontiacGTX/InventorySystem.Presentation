using InventorySystem.Data;
using InventorySystem.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Command
{
    public class CreateProductoCommand: IPropertyFieldHolder<Producto>, IRequest<Producto>
    {
        public Producto Field { get;set; }

        public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, Producto>
        {
            protected AppDbContext _ctx { get; }
            public CreateProductoCommandHandler(AppDbContext context)
            {
                _ctx = context;
            }
            public async Task<Producto> Handle(CreateProductoCommand command, CancellationToken cancellationToken)
            {
                var product = new Producto();
                product.Id = command.Field.Id;
                product.CodigoDeBarra = command.Field.CodigoDeBarra;
                product.Nombre = command.Field.Nombre;
                product.PrecioUnitario = command.Field.PrecioUnitario;
                product.CantidadDisponible = command.Field.CantidadDisponible;
                _ctx.Productos.Add(product);
                await _ctx.SaveChangesAsync();
                return product;
            }
        }
    }
}
