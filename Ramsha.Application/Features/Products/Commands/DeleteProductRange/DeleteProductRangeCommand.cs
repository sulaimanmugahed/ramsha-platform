
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.DeleteProductRange;

public class DeleteProductRangeCommand : IRamshaCommand
{
    public List<Guid> Products { get; set; }
}
