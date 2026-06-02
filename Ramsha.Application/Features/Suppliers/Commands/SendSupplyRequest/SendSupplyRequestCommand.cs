
using Ramsha.Domain.Products.Enums;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Commands.SendSupplyRequest;

public class SendSupplyRequestCommand : IRamshaCommand
{
    public CurrencyCode Currency { get; set; }
}
