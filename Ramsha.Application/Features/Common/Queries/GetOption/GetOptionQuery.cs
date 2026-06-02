
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Common.Queries.GetOption;

public class GetOptionQuery : IRamshaQuery
{
    public Guid Id { get; set; }
}
