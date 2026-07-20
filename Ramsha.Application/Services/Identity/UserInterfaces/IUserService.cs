using Ramsha.Application.Constants;
using Ramsha.Application.Dtos.Account.Requests;
using Ramsha.Application.Dtos.Account.Responses;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Common;
using Ramsha.Shared;


namespace Ramsha.Application.Services.Identity.UserInterfaces;

public interface IUserService
{
	Task<RamshaResult<RegisterResponse>> CreateAccount(RegisterRequest request, string roleName = Roles.Customer, List<PermissionType>? permissions = null, bool emailConfirmed = false, bool phoneNumberConfirmed = false);
	Task<RamshaResult<AccountDto>> GetAccount(string username);
	Task<RamshaResult<List<AccountDto>>> GetAccounts(string? roleName = null);
	Task<IRamshaResult> DeleteAccount(string username);
	Task<Address?> GetUserAddress(string userName);
	Task UpdateUserAddress(string userName, Address address);


}
