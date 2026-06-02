
using Ramsha.Application.Dtos.Account.Requests;
using Ramsha.Application.DTOs.Account.Requests;
using Ramsha.Application.DTOs.Account.Responses;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Common;

namespace Ramsha.Application.Contracts.Identity
{
    public interface IAccountServices
    {
        Task<bool> ChangePassword(string username, ChangePasswordRequest request);
        Task<IRamshaResult> ChangeUserName(ChangeUserNameRequest model);
        Task<RamshaResult<AuthenticationResult>> Authenticate(AuthenticationRequest request);
        Task<RamshaResult<AuthenticationResult>> AuthenticateByUserName(string username);
        Task<RamshaResult<AuthenticationResult>> GetCurrentAccount();
        Task<RamshaResult<AuthenticationResult>> Refresh(string token, string? accessToken = null);
        Task<IRamshaResult> Revoke(string token);
        Task LogoutCurrentUser();
        Task UpdateAddress(string userName, Address address);
        Task<bool> SendConfirmEmail(string userEmail);
        Task<bool> VerifyEmail(string userEmail, string token);
        Task<bool> SendResetPasswordEmail(string username);
        Task<bool> ResetPassword(string username, ResetPasswordRequest request);

    }
}
