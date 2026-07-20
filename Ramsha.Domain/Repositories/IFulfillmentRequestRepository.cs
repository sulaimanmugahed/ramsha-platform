using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ramsha.Common.Domain;
using Ramsha.Domain.Orders;
using Ramsha.Domain.Orders.Entities;
using Ramsha.Domain.Suppliers;
using Ramsha.Domain.Suppliers.Entities;

namespace Ramsha.Domain.Repositories;

public interface IFulfillmentRequestRepository : IRepository<FulfillmentRequest, FulfillmentRequestId>
{
    // Task<PaginationResponseDto<FulfillmentRequestDto>> GetPaged(Ramsha.Application.Wrappers.PaginationParams paginationParams, FilterParams? filterParams, SortingParams? sortingParams, Expression<Func<FulfillmentRequest, bool>>? criteria = null);
}
