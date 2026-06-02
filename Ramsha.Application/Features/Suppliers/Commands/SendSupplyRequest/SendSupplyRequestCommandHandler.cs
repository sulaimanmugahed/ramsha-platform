
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Suppliers.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Commands.SendSupplyRequest;

public class SendSupplyRequestCommandHandler(

    ISupplyRequestRepository supplyRequestRepository,
    IAuthenticatedUserService authenticatedUser,
    ISupplyRepository supplyRepository
) : RamshaCommandHandler<SendSupplyRequestCommand>
{
    public override async Task<IRamshaResult> HandleAsync(SendSupplyRequestCommand request, CancellationToken cancellationToken)
    {
        if (authenticatedUser.UserName is null)
            return Unauthenticated();

        var supplyRequest = await supplyRequestRepository.GetWithDetails(
           x => x.Supplier == authenticatedUser.UserName);

        if (supplyRequest is null)
            return Invalid(message: "no supplyRequest was found");

        var supply = Supply.Create(authenticatedUser.UserName, request.Currency);
        int totalQuantity = 0;
        decimal totalPrice = 0;



        foreach (var item in supplyRequest.Items)
        {
            var itemSupplied = new ItemSupplied(item.ProductId, item.ProductVariantId, item.Product.Name, item.SupplierVariant.Code);
            var supplyItem = SupplyItem.Create(itemSupplied, item.SupplierVariant.WholesalePrice, item.Quantity);
            supply.AddItem(supplyItem);
            totalQuantity += item.Quantity;
            totalPrice += item.SupplierVariant.WholesalePrice * item.Quantity;
        }
        supply.SetTotal(totalPrice, totalQuantity);

        await supplyRepository.AddAsync(supply);

        await supplyRequestRepository.DeleteAsync(supplyRequest);

        return Success();
    }
}
