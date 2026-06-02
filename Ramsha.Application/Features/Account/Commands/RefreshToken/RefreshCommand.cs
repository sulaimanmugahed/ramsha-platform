using Ramsha.Application.DTOs.Account.Responses;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;


namespace Ramsha.Application.Features.Account.Commands.RefreshToken;

public class RefreshCommand : IRamshaCommand
{
    public string? AccessToken { get; set; }
}
