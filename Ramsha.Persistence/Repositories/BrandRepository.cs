
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Products.Entities;
using Ramsha.Domain.Products;

using Ramsha.Persistence.Contexts;
using Ramsha.EntityFrameworkCore;

namespace Ramsha.Persistence.Repositories;

public class BrandRepository
: EFCoreRepository<ApplicationDbContext, Brand, BrandId>, IBrandRepository
{

}
