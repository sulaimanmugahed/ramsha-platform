
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRamshaCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
