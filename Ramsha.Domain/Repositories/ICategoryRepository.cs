
using Ramsha.Common.Domain;
using Ramsha.Domain.Products;
using Ramsha.Domain.Products.Entities;

namespace Ramsha.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category, CategoryId>
{
    Task<List<Category>> GetMainCategories();

    Task<List<CatalogCategoryDto>> GetCatalogCategories();
    Task<List<TotalCategoryProducts>> GetTotalMainCategoriesProducts();

    Task<List<CategoryId>> GetChildCategoryIds(CategoryId categoryId);


}
