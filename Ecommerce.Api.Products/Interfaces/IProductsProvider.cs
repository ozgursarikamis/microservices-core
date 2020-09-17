using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Api.Products.Models;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
