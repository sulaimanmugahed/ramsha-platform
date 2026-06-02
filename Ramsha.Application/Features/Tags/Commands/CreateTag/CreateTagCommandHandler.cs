
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Tags.Commands.CreateTag;

public class CreateTagCommandHandler(
    ITagRepository tagRepository
) : RamshaCommandHandler<CreateTagCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = Tag.Create(request.Name);

        await tagRepository.AddAsync(tag);

        return Success(tag.Id.Value.ToString());
    }
}
