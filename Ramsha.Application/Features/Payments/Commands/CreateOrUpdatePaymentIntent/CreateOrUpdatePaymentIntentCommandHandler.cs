
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Contracts.Payment;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Extensions;
using Ramsha.Application.Services;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Payments.Commands.CreateOrUpdatePaymentIntent;

public class CreateOrUpdatePaymentIntentCommandHandler(
    BasketService basketService,
    IBasketRepository basketRepository,
    IAuthenticatedUserService authenticatedUserService,
    IUserService userService,
    IPaymentService paymentService

) : RamshaCommandHandler<CreateOrUpdatePaymentIntentCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateOrUpdatePaymentIntentCommand request, CancellationToken cancellationToken)
    {
        var customerAddress = await userService.GetUserAddress(authenticatedUserService.UserName);
        if (customerAddress is null)
            return Invalid(message: "no customer Address found");

        var basket = await basketRepository.GetDetail(authenticatedUserService.UserName);

        if (basket is null)
            return Invalid(message: "no basket found");

        var subtotal = basket.Items.Sum(item => item.Quantity * item.InventoryItem.FinalPrice.Amount);

        var deliveryFee = await basketService.CalculateTotalDeliveryFees(basket, customerAddress);

        var finalPrice = subtotal + deliveryFee;

        var intentResponse = await paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId, finalPrice);

        if (intentResponse is null)
            return Invalid(message: "error while update the payment");


        basket.PaymentIntentId ??= intentResponse.PaymentIntentId;
        basket.ClientSecret ??= intentResponse.ClientSecret;


        return Success(basket.ToDto());
    }
}
