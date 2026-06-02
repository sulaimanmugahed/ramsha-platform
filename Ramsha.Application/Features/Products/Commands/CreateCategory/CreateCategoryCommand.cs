
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.CreateCategory;

public class CreateCategoryCommand : IRamshaCommand
{
    public string Name { get; set; }
    public Guid ParentId { get; set; }

}
