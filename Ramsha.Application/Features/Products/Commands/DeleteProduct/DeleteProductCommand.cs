
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRamshaCommand
{
    public Guid Id { get; set; }
}
