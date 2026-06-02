


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Common;
using Ramsha.Domain.Customers.Entities;
using Ramsha.Domain.DeliveryAgents;
using Ramsha.Domain.Inventory.Entities;
using Ramsha.Domain.Orders.Entities;
using Ramsha.Domain.Products.Entities;
using Ramsha.Domain.Suppliers.Entities;
using Ramsha.Persistence.Contexts;
using Ramsha.Persistence.Repositories;
using System.Reflection;


namespace Ramsha.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRamshaDbContext<ApplicationDbContext>(options =>
        {
            options
            .AddDefaultRepositories()
            .AddRepository<Basket, IBasketRepository, BasketRepository>()
            .AddRepository<Brand, IBrandRepository, BrandRepository>()
            .AddRepository<Category, ICategoryRepository, CategoryRepository>()
            .AddRepository<Coupon, ICouponRepository, CouponRepository>()
            .AddRepository<Currency, ICurrencyRateRepository, CurrencyRateRepository>()
            .AddRepository<Customer, ICustomerRepository, CustomerRepository>()
            .AddRepository<DeliveryAgent, IDeliveryAgentRepository, DeliveryAgentRepository>()
            .AddRepository<FulfillmentRequest, IFulfillmentRequestRepository, FulfillmentRequestRepository>()
            .AddRepository<InventoryItem, IInventoryItemRepository, InventoryItemRepository>()
            .AddRepository<Option, IOptionRepository, OptionRepository>()
            .AddRepository<Order, IOrderRepository, OrderRepository>()
            .AddRepository<Product, IProductRepository, ProductRepository>()
            .AddRepository<Stock, IStockRepository, StockRepository>()
            .AddRepository<SupplierProduct, ISupplierProductRepository, SupplierProductRepository>()
            .AddRepository<Supplier, ISupplierRepository, SupplierRepository>()
            .AddRepository<Supply, ISupplyRepository, SupplyRepository>()
            .AddRepository<SupplyRequest, ISupplyRequestRepository, SupplyRequestRepository>()
            .AddRepository<Tag, ITagRepository, TagRepository>();
        });

        return services;

    }

}
