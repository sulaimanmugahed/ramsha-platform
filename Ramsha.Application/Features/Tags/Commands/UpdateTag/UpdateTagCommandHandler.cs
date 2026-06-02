
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Tags.Commands.UpdateTag;

public class UpdateTagCommandHandler(
    ITagRepository tagRepository
) : RamshaCommandHandler<UpdateTagCommand>
{
    public override async Task<IRamshaResult> HandleAsync(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var existTag = await tagRepository.FindAsync(new TagId(request.Id));
        if (existTag is null)
            return Invalid(message: "no tag with this id");

        existTag.Update(request.Name);
        return Success();
    }
}
