using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;


namespace Ramsha.Application.Features.Common.Commands.AddNewOption;

public class AddNewOptionCommandHandler(
    IOptionRepository optionRepository)
    : RamshaCommandHandler<AddNewOptionCommand>
{
    public override async Task<IRamshaResult> HandleAsync(AddNewOptionCommand request, CancellationToken cancellationToken)
    {
        var option = Option.Create(request.OptionName);
        option.AddValues(request.OptionValues);

        await optionRepository.AddAsync(option);

        return Success(option.Id.Value.ToString());
    }
}
