using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Queries.GetVariantDetails;

public class GetVariantDetailsQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }

    public Guid ProductVariantId { get; set; }
}
