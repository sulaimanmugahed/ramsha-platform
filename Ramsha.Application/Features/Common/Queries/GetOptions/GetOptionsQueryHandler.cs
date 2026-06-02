
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Common.Queries.GetOption;

public class GetOptionsQueryHandler(IOptionRepository optionRepository)
 : RamshaQueryHandler<GetOptionsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetOptionsQuery request, CancellationToken cancellationToken)
    {
        var options = await optionRepository.GetListAsync(
            [x => x.OptionValues]);

        return Success(options.Select(x => x?.AsDto()).ToList());
    }
}
