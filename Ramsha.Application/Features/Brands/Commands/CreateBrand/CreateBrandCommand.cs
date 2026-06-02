
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRamshaCommand
{
    public string Name { get; set; }
}
