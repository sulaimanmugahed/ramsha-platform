using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Products;
using Ramsha.Domain.Products.Entities;
using Ramsha.Persistence.Contexts;
using Ramsha.Application.Dtos.Catalog;
using Ramsha.Application.Extensions;
using Ramsha.Application.Dtos.Statistics;
using Ramsha.EntityFrameworkCore;

namespace Ramsha.Persistence.Repositories;

public class CategoryRepository
: EFCoreRepository<ApplicationDbContext, Category, CategoryId>, ICategoryRepository
{

    public async Task<List<Category>> GetMainCategories()
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Categories.Where(c => c.ParentCategoryId == null).Include(x => x.SubCategories).ToListAsync();
    }

    public async Task<List<CategoryId>> GetChildCategoryIds(CategoryId categoryId)
    {
        var dbContext = await GetDbContextAsync();
        var categoryIds = await dbContext.Categories
            .Where(c => c.Id == categoryId || c.ParentCategoryId == categoryId)
            .Select(c => c.Id)
            .ToListAsync();

        return categoryIds;
    }

    public async Task<List<CatalogCategoryDto>> GetCatalogCategories()
    {
        var dbContext = await GetDbContextAsync();
        var categories = dbContext.Categories
        .Include(x => x.SubCategories)
        .Include(x => x.Products)
        .AsQueryable();
        return await categories.Select(x => x.AsCatalogCategoryDto()).ToListAsync();
    }

    public async Task<List<TotalCategoryProducts>> GetTotalMainCategoriesProducts()
    {
        var dbContext = await GetDbContextAsync();
        var categories = await dbContext.Categories
            .Include(x => x.SubCategories)
            .Include(x => x.Products)
            .Where(x => x.ParentCategoryId == null)
            .ToListAsync();

        var result = categories
            .Select(x => new TotalCategoryProducts(x.Name, GetTotalProductCount(x)))
            .ToList();

        return result;
    }

    private int GetTotalProductCount(Category category)
    {
        int totalCount = category.Products.Count;

        foreach (var subCategory in category.SubCategories)
        {
            totalCount += GetTotalProductCount(subCategory);
        }

        return totalCount;
    }
}
