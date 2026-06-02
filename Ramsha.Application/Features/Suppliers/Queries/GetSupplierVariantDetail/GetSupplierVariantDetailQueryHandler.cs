
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetSupplierVariantDetail;

public class GetSupplierVariantDetailQueryHandler(
) : RamshaQueryHandler<GetSupplierVariantDetailQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetSupplierVariantDetailQuery request, CancellationToken cancellationToken)
    {
        return Success();
    }
}
