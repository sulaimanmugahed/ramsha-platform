
using Ramsha.Common.Domain;
using Ramsha.Domain.Common;

namespace Ramsha.Domain.Products.Entities;

public class Category : Entity<CategoryId>
{
    private Category(CategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
    public string Code { get; set; }
    public CategoryId? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Category> SubCategories { get; set; } = [];
    public List<Product> Products { get; set; } = [];


    public static Category Create(string name)
    {
        return new Category(new CategoryId(Guid.NewGuid()), name);
    }

    public void SetName(string name)
    {
        Name = name;


    }

    public void AddChild(Category childCategory)
    {
        SubCategories.Add(childCategory);
    }

    public void SetParent(CategoryId parentCategoryId)
    {
        ParentCategoryId = parentCategoryId;
    }

    public void SetCode(string code)
    {
        Code = code;
    }

}
