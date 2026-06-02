
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Commands.AddSupplierProduct;

public class AddSupplierProductCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }

}
