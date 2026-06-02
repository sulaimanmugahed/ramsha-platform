
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;


namespace Ramsha.Application.Features.Products.Commands.CreateCategory;

public class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    ICodeGenerator codeGenerator
) : RamshaCommandHandler<CreateCategoryCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = Category.Create(request.Name);

        if (request.ParentId != Guid.Empty)
        {
            newCategory.SetParent(new Domain.Products.CategoryId(request.ParentId));
        }

        newCategory.SetCode(codeGenerator.GenerateCategoryCode(newCategory.Id.Value));

        await categoryRepository.AddAsync(newCategory);

        return Success(newCategory.Id.Value.ToString());
    }
}
