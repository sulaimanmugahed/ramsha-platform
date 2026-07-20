
using Ramsha.Application.Contracts.Email;

namespace Ramsha.Contracts;

public interface IEmailService
{
	Task SendEmailMessage(EmailMessage emailMessage);
}
