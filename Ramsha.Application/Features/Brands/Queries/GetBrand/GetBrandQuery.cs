
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Brands.Queries.GetBrand;

public class GetBrandQuery : IRamshaQuery
{
    public Guid Id { get; set; }
}
