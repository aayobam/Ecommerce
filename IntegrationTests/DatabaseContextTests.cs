using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContexts;

namespace IntegrationTests;

public class DatabaseContextTests
{
    private readonly EcommerceDbContext _ecommerceDbContext;

    public DatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<EcommerceDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _ecommerceDbContext = new EcommerceDbContext(dbOptions);
    }
     
    [Fact]
    public async void SaveSetDateCreatedValue()
    {
        var category = new Category
        { 
            Id = Guid.Parse(""),
            Name = "Electronics"
        };

        await _ecommerceDbContext.Categories.AddAsync(category);
        _ecommerceDbContext.SaveChanges();

        //category.DateCreated.ShouldNotBeNull<DateTimeOffset>(); 
    }

    [Fact]
    public async void SaveSetDateModifiedValue()
    {
        var category = new Category
        {
            Id = Guid.Parse(""),
            Name = "Electronics"
        };

        await _ecommerceDbContext.Categories.AddAsync(category);
        _ecommerceDbContext.SaveChanges();

        //category.DateModified.ShouldNotBeNull();
    }
}
