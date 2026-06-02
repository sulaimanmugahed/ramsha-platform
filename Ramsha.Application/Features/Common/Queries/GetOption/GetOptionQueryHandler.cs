using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Common.Queries.GetOption;

public class GetOptionQueryHandler(IOptionRepository optionRepository)
 : RamshaQueryHandler<GetOptionQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetOptionQuery request, CancellationToken cancellationToken)
    {
        var option = await optionRepository.FindAsync(
            x => x.Id.Value == request.Id,
           [x => x.OptionValues]);

        return Success(option?.AsDto());
    }
}
