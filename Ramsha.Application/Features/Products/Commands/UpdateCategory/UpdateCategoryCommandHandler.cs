
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
ICategoryRepository categoryRepository
) : RamshaCommandHandler<UpdateCategoryCommand>
{
    public override async Task<IRamshaResult> HandleAsync(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindAsync(new Domain.Products.CategoryId(request.Id));
        if (category is null)
            return Invalid(message: "no category was found");

        category.SetName(request.Name);

        return Success();
    }
}
