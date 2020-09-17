using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            await Task.Delay(1);
            return (true, new {Message = "Hello"});
        }
    }
}
