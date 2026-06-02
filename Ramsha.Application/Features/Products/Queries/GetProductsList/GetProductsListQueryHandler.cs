using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductsList;

public class GetProductsListQueryHandler(
    IProductRepository productRepository


) : RamshaQueryHandler<GetProductsListQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetListAsync(
         [x => x.Options,
         x => x.Category,
         X => X.Inventories]
        );


        return Success(products.Select(p => p?.AsDto()).ToList());
    }
}
