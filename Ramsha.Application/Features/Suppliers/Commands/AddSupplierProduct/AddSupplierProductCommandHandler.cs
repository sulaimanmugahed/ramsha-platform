
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Commands.AddSupplierProduct;

public class AddSupplierProductCommandHandler(
    ISupplierRepository supplierRepository,
    ISupplierProductRepository supplierProductRepository,
    IProductRepository productRepository,
    IAuthenticatedUserService authenticatedUserService,
    ICodeGenerator codeGenerator

) : RamshaCommandHandler<AddSupplierProductCommand>
{
    public override async Task<IRamshaResult> HandleAsync(AddSupplierProductCommand request, CancellationToken cancellationToken)
    {
        var productId = new Domain.Products.ProductId(request.ProductId);
        var supplier = await supplierRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (supplier is null)
            return Unauthenticated();

        var product = await productRepository.FindAsync(productId);

        if (product is null)
        {
            return Invalid(message: "invalid product id");
        }

        var supplierProduct = SupplierProduct.Create(product.Id, supplier.Id);

        supplierProduct.SetCode(codeGenerator.GenerateSupplierProductCode(supplier.Username, product.Code));

        await supplierProductRepository.AddAsync(supplierProduct);

        return Success();

    }
}
