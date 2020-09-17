using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Customer = Ecommerce.Api.Customers.Models.Customer;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider: ICustomersProvider
    {
        private readonly CustomersDbContext _context;
        private readonly ILogger<CustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext context, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Db.Customer { Id = 1, Name = "Jessica Smith", Address = "20 Elm St." });
                _context.Customers.Add(new Db.Customer { Id = 2, Name = "John Smith", Address = "30 Main St." });
                _context.Customers.Add(new Db.Customer { Id = 3, Name = "William Johnson", Address = "100 10th St." });

                _context.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {

                _logger.LogError("Querying customers");
                var customers = await _context.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    _logger.LogInformation($"{customers.Count} customer(s) found...");
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Customer>>(customers);
                    return (true, result, null);
                }

                return (false, null, "No customers found");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                _logger?.LogInformation("Querying customers");
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    _logger?.LogInformation("Customer found");
                    var result = _mapper.Map<Db.Customer, Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
