using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;


namespace Ramsha.Application.Features.Common.Commands.RemoveOption;

public class RemoveOptionCommandHandler(
    IOptionRepository optionRepository,
    IProductRepository productRepository)
 : RamshaCommandHandler<RemoveOptionCommand>
{
    public override async Task<IRamshaResult> HandleAsync(RemoveOptionCommand request, CancellationToken cancellationToken)
    {
        var optionId = new Domain.Products.OptionId(request.OptionId);

        var option = await optionRepository.FindAsync(optionId);
        if (option is null)
            return NotFound(message: "No option found");

        var exist = await productRepository.IsOptionExist(optionId);
        if (exist)
            return Invalid(message: "There are some variants take this option");

        await optionRepository.DeleteAsync(option);
        return Success();
    }
}
