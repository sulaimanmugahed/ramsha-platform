
using System.Linq.Expressions;

using Ramsha.Common.Domain;
using Ramsha.Domain.Orders;
using Ramsha.Domain.Orders.Entities;

namespace Ramsha.Domain.Repositories;

public interface IOrderRepository : IRepository<Order, OrderId>
{
    Task<IEnumerable<Order>> FindOrdersWithDetails(Expression<Func<Order, bool>> criteria);
    Task<Order?> FindOrderWithDetails(Expression<Func<Order, bool>> criteria);

    // Task<PaginationResponseDto<OrderDto>> GetPaged(PagedParams pagedParams, Expression<Func<Order, bool>>? criteria = null);
}
