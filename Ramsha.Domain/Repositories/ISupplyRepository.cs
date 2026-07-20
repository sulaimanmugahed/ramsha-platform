using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Ramsha.Common.Domain;
using Ramsha.Domain.Suppliers;
using Ramsha.Domain.Suppliers.Entities;

namespace Ramsha.Domain.Repositories;

public interface ISupplyRepository : IRepository<Supply, SupplyId>
{
    Task<IEnumerable<Supply>> FindAllWithDetail(Expression<Func<Supply, bool>> criteria);
    Task<Supply?> GetWithDetails(Expression<Func<Supply, bool>> criteria);

    //Task<PaginationResponseDto<SupplyDto>> GetSuppliesPaged(PagedParams pagedParams, Expression<Func<Supply, bool>>? criteria = null);

}
