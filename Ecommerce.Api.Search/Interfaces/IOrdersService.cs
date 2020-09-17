using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> 
            GetOrdersAsync(int customerId);
    }
}