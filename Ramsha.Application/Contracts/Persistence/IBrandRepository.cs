using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Domain.Products.Entities;
using Ramsha.Domain.Products;
using Ramsha.Common.Domain;


namespace Ramsha.Application.Contracts.Persistence;

public interface IBrandRepository : IRepository<Brand, BrandId>
{

}
