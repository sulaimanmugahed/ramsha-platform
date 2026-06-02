
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Orders.Entities;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRamshaCommand
{
    public ShippingAddress? ShippingAddress { get; set; }
}
