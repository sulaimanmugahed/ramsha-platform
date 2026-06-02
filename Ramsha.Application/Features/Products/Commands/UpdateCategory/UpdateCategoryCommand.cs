
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRamshaCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }

}
