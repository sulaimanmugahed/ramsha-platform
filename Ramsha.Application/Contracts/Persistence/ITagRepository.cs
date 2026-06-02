using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Common.Domain;
using Ramsha.Domain.Products.Entities;

namespace Ramsha.Application.Contracts.Persistence;

public interface ITagRepository : IRepository<Tag, TagId>
{

}
