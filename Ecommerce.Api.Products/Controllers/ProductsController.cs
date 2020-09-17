using System.Threading.Tasks;
using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Products.Controllers
{
    [ApiController, Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider provider;

        public ProductsController(IProductsProvider provider)
        {
            this.provider = provider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var (isSuccess, products, errorMessage) = await provider.GetProductsAsync();
            if (isSuccess)
            {
                return Ok(products);
            }

            return NotFound(errorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsAsync(int id)
        {
            var result = await provider.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }

            return NotFound();
        }
    }
}
