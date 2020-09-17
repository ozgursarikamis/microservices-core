using System;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace Ecommerce.Api.Products.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            //var context = new ProductsDbContext();
            //var productsProvider = new ProductsProvider();

            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;

            var context = new ProductsDbContext(options);
            CreateProducts(context);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(config =>
                config.AddProfile(productProfile));

            var mapper = new Mapper(configuration);

            var provider = new ProductsProvider(context, null, mapper);

            var (isSuccess, products, errorMessage) = await provider.GetProductsAsync();

            Assert.True(isSuccess);
            Assert.True(products.Any());
            Assert.Null(errorMessage);
        }

        private static void CreateProducts(ProductsDbContext context)
        {
            for (var i = 1; i < 10; i++)
            {
                var product = new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                };
                context.Products.Add(product);
                context.SaveChanges();
            }

        }
    }
}
