using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Customers;
using Ramsha.Domain.Customers.Entities;
using Ramsha.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Ramsha.Application.DTOs.Common;
using Ramsha.Application.Dtos.Customers;
using Ramsha.Application.Wrappers;
using System.Linq.Expressions;
using Ramsha.Persistence.Helpers;
using Ramsha.Application.Extensions;
using Ramsha.EntityFrameworkCore;


namespace Ramsha.Persistence.Repositories;

public class CustomerRepository
 : EFCoreRepository<ApplicationDbContext, Customer, CustomerId>, ICustomerRepository
{
	public async Task<Customer?> FindByUsername(string username)
	{
		return await UnitOfWork(async () =>
	   {
		   var dbContext = await GetDbContextAsync();
		   return await dbContext.Customers.FirstOrDefaultAsync(x => x.Username == username);
	   });
	}


	public async Task<PaginationResponseDto<CustomerDto>> GetPaged(PagedParams pagedParams, Expression<Func<Customer, bool>>? criteria = null)
	{
		return await UnitOfWork<PaginationResponseDto<CustomerDto>>(async () =>
	   {
		   var dbContext = await GetDbContextAsync();
		   var customersQuery = dbContext.Customers
		.AsQueryable();

		   var sortingParams = pagedParams.SortingParams;
		   customersQuery = sortingParams is not null && sortingParams.ColumnsSort.Count > 0
			 ? customersQuery.OrderByColumnName(sortingParams.ColumnsSort)
			 : customersQuery.OrderBy(x => x.CreationDate);


		   if (criteria is not null)
		   {
			   customersQuery = customersQuery.Where(criteria);
		   }

		   var filterParams = pagedParams.FilterParams;
		   if (filterParams is not null)
		   {
			   var globalFilter = filterParams.GlobalFilterValue;

			   if (!string.IsNullOrEmpty(globalFilter))
			   {

			   }

			   if (filterParams.ColumnsFilter is not null && filterParams.ColumnsFilter.Count != 0)
				   customersQuery = customersQuery.FilterByColumn(filterParams.ColumnsFilter);


		   }



		   var query = customersQuery.Select(p => p.AsDto());
		   var count = await query.CountAsync();

		   var paginationParams = pagedParams.PaginationParams;
		   var pagedResult = await query
				.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
				.Take(paginationParams.PageSize)
				.ToListAsync();

		   return new(pagedResult, paginationParams, count);
	   });


	}
}
