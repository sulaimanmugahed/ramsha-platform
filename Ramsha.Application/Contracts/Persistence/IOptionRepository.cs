
using Ramsha.Common.Domain;
using Ramsha.Domain.Products;
using Ramsha.Domain.Products.Entities;

namespace Ramsha.Application.Contracts.Persistence;

public interface IOptionRepository : IRepository<Option, OptionId>
{
    Task<OptionValue?> GetValue(OptionValueId optionValueId);
}
