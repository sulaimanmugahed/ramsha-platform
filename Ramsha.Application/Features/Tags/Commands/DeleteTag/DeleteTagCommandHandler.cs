
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Tags.Commands.DeleteTag;

public class DeleteTagCommandHandler(
        ITagRepository tagRepository
) : RamshaCommandHandler<DeleteTagCommand>
{
    public override async Task<IRamshaResult> HandleAsync(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var existTag = await tagRepository.FindAsync(new TagId(request.Id));
        if (existTag is null)
            return Invalid(message: "no tag with this id");

        await tagRepository.DeleteAsync(existTag);

        return Success();
    }
}
