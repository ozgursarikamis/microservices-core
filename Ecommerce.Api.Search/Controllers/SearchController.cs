using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Search.Controllers
{
    [ApiController, Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync([FromBody]SearchTerm term)
        {
            var (isSuccess, searchResults) = await _searchService.SearchAsync(term.CustomerId);
            return isSuccess ? (IActionResult) Ok(searchResults) : NotFound();
        }
    }
}
