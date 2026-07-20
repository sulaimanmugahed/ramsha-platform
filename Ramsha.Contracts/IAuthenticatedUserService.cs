namespace Ramsha.Contracts;

public interface IAuthenticatedUserService
{
    string UserId { get; }
    string UserName { get; }
    string Role { get; }
}
