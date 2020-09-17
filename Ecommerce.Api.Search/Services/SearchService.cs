using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        public SearchService(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            
            if (!ordersResult.IsSuccess) return (false, null);
            var result = new { ordersResult.Orders};

            return (true, result);

        }
    }
}
