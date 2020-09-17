using System.Threading.Tasks;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController: ControllerBase
    {
        private readonly ICustomersProvider _customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            _customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var (isSuccess, customers, _) = await _customersProvider.GetCustomersAsync();
            if (isSuccess)
            {
                return Ok(customers);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var (isSuccess, customer, _) = await _customersProvider.GetCustomerAsync(id);
            if (isSuccess)
            {
                return Ok(customer);
            }
            return NotFound();
        }
    }
}
