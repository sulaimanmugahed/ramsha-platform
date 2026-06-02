
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Products;
using Ramsha.Domain.Products.Entities;
using Ramsha.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Ramsha.EntityFrameworkCore;

namespace Ramsha.Persistence.Repositories;

public class OptionRepository
: EFCoreRepository<ApplicationDbContext, Option, OptionId>,
IOptionRepository
{


    public async Task<OptionValue?> GetValue(OptionValueId optionValueId)
    {
        var context = await GetDbContextAsync();

        return await context.Set<OptionValue>().FirstOrDefaultAsync(x => x.Id == optionValueId);
    }
}
