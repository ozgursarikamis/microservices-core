using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public SearchService(IOrdersService ordersService, IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.Products
                            .FirstOrDefault(p => p.Id == item.ProductId)?.Name;
                    }
                }
            }

            
            if (!ordersResult.IsSuccess) return (false, null);
            var result = new { ordersResult.Orders };

            return (true, result);

        }
    }
}
