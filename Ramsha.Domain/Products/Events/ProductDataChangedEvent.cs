using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramsha.Domain.Products.Events;

public record ProductDataChangedEvent(
    ProductId ProductId
)
{

}
