
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.RemoveCategory;

public class RemoveCategoryCommand : IRamshaCommand
{
    public Guid Id { get; set; }
}
