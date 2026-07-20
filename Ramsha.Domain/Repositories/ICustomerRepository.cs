
using Ramsha.Common.Domain;
using Ramsha.Domain.Customers;
using Ramsha.Domain.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ramsha.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer, CustomerId>
{
	Task<Customer?> FindByUsername(string username);
	//Task<PaginationResponseDto<CustomerDto>> GetPaged(PagedParams pagedParams, Expression<Func<Customer, bool>>? criteria = null);
}
