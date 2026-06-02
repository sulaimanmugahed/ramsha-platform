using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Common.Domain;
using Ramsha.Domain.Common;

namespace Ramsha.Domain.Products.Entities;

public class Tag : Entity<TagId>
{
    public Tag()
    {

    }

    private Tag(TagId id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public List<ProductTag> ProductTags { get; private set; } = [];

    public static Tag Create(string tag)
    => new(new TagId(Guid.NewGuid()), tag);
}
