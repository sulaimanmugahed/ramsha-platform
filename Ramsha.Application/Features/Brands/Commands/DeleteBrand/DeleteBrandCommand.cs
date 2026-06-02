
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRamshaCommand
{
    public Guid Id { get; set; }
}
