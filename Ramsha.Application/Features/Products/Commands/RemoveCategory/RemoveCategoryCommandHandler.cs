
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.RemoveCategory;

public class RemoveCategoryCommandHandler(
    ICategoryRepository categoryRepository
) : RamshaCommandHandler<RemoveCategoryCommand>
{
    public override async Task<IRamshaResult> HandleAsync(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindAsync(new Domain.Products.CategoryId(request.Id));
        if (category is null)
            return Invalid(message: "no category was found");

        await categoryRepository.DeleteAsync(category);


        return Success();
    }
}
