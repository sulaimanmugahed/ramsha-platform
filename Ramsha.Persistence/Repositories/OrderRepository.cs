using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Orders;
using Ramsha.Domain.Orders.Entities;
using Ramsha.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Ramsha.Application.DTOs.Common;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Wrappers;
using Ramsha.Application.Extensions;
using Ramsha.Persistence.Helpers;
using Ramsha.EntityFrameworkCore;

namespace Ramsha.Persistence.Repositories;

public class OrderRepository
 : EFCoreRepository<ApplicationDbContext, Order, OrderId>,
 IOrderRepository
{

    public async Task<IEnumerable<Order>> FindOrdersWithDetails(Expression<Func<Order, bool>> criteria)
    {
        return await UnitOfWork(async () =>
{
    var context = await GetDbContextAsync();

    return await context.Orders
        .Include(x => x.OrderItems)
        .ThenInclude(x => x.ItemOrdered)
        .Where(criteria).ToListAsync();
});

    }

    public async Task<Order?> FindOrderWithDetails(Expression<Func<Order, bool>> criteria)
    {
        return await UnitOfWork(async () =>
{
    var context = await GetDbContextAsync();

    return await context.Orders
        .Include(x => x.OrderItems)
        .ThenInclude(x => x.ItemOrdered)
        .FirstOrDefaultAsync(criteria);
});

    }

    public async Task<PaginationResponseDto<OrderDto>> GetPaged(PagedParams pagedParams, Expression<Func<Order, bool>>? criteria = null)
    {
        return await UnitOfWork<PaginationResponseDto<OrderDto>>(async () =>
{
    var context = await GetDbContextAsync();

    var ordersQuery = context.Orders.AsQueryable();

    if (criteria is not null)
        ordersQuery = ordersQuery.Where(criteria);


    var sortingColumn = pagedParams.SortingParams?.ColumnsSort;
    if (sortingColumn.HasItems())
    {
        ordersQuery = ordersQuery.OrderByColumnName(sortingColumn);
    }
    else
    {
        ordersQuery = ordersQuery.OrderBy(x => x.OrderStatus);
    }

    var filterColumn = pagedParams.FilterParams?.ColumnsFilter;
    if (filterColumn.HasItems())
    {
        ordersQuery = ordersQuery.FilterByColumn(filterColumn);
    }

    var count = await ordersQuery.CountAsync();
    var paginationParams = pagedParams.PaginationParams;
    var pagedResult = await ordersQuery
        .Select(p => p.AsDto())
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

    return new(pagedResult, paginationParams, count);
});
    }
}
