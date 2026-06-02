using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Dtos.Account.Requests;
using Ramsha.Application.Dtos.Account.Responses;
using Ramsha.Application.Wrappers;
using Ramsha.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Ramsha.Domain.Common;
using Ramsha.Application.Contracts.Identity;
using Ramsha.Domain.Constants;
using Ramsha.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using Ramsha.Application.Constants;

namespace Ramsha.Identity.Services;

public class UserService(UserManager<Account> userManager, RoleManager<ApplicationRole> roleManager) : IUserService
{
	public async Task<IRamshaResult> DeleteAccount(string username)
	{
		var user = await userManager.FindByNameAsync(username);
		if (user is null)
			return RamshaResults.NotFound();


		await userManager.DeleteAsync(user);

		return RamshaResults.Success();
	}

	public async Task<Address?> GetUserAddress(string userName)
	{
		var user = await userManager.FindByNameAsync(userName);
		return user?.Address;
	}

	public async Task UpdateUserAddress(string userName, Address address)
	{
		var user = await userManager.FindByNameAsync(userName);
		if (user is not null)
		{
			user.Address = address;
			await userManager.UpdateAsync(user);
		}
	}


	public async Task<RamshaResult<AccountDto>> GetAccount(string username)
	{
		var account = await userManager.FindByNameAsync(username);
		if (account is null)
			return RamshaResults.NotFound();


		return new AccountDto(
			account.Id,
			username,
			account.Email);
	}


	public async Task<RamshaResult<RegisterResponse>> CreateAccount(RegisterRequest request, string roleName = Roles.Customer, List<PermissionType>? permissions = null, bool emailConfirmed = false, bool phoneNumberConfirmed = false)
	{
		var account = new Account
		{
			Email = request.Email,
			UserName = request.Username,
			PreferredCurrency = request.PreferredCurrency,
			Created = DateTime.UtcNow,
			EmailConfirmed = emailConfirmed,
			PhoneNumberConfirmed = phoneNumberConfirmed
		};

		var result = await userManager.CreateAsync(account, request.Password);

		if (!result.Succeeded)
		{
			// var errors = result.Errors.Select(e => new Error(ErrorCode.NotFound, e.Description)).ToList();
			// return new List<Error>(errors);

			return RamshaResults.Invalid(message: result.Errors.FirstOrDefault()?.Description);
		}


		var role = await roleManager.Roles.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Name == roleName);
		if (role is null)
			return RamshaResults.Invalid(message: "EmptyData");

		var addToRoleResult = await userManager.AddToRoleAsync(account, roleName);
		if (!addToRoleResult.Succeeded)
		{
			// var addToRoleError = addToRoleResult.Errors.Select(e => new Error(ErrorCode.NotFound, e.Description)).ToList();
			// return new List<Error>(addToRoleError);
			return RamshaResults.Invalid(message: addToRoleResult.Errors.FirstOrDefault()?.Description);

		}

		if (permissions is not null)
		{
			account.AddPermissions(role.Permissions.Where(x => permissions.Contains(x.Type)));
		}
		else
		{
			account.AddPermissions(role.Permissions);
		}


		await userManager.UpdateAsync(account);


		return new RegisterResponse { Id = account.Id };
	}

	public async Task<RamshaResult<List<AccountDto>>> GetAccounts(string? roleName = null)
	{
		var accounts = roleName is null ? await userManager.Users.ToListAsync() : await userManager.GetUsersInRoleAsync(roleName);
		return accounts.Select(x => new AccountDto(x.Id, x.UserName, x.Email)).ToList();
	}


}
