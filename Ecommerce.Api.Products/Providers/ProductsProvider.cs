﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Product = Ecommerce.Api.Products.Db.Product;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext context, ILogger logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (EnumerableExtensions.Any(_context.Products)) return;
            _context.Products.Add(new Product {Id = 1, Name = "Keyboard", Inventory = 10, Price = 123 });
            _context.Products.Add(new Product {Id = 2, Name = "Mouse", Inventory = 10, Price = 45 });
            _context.Products.Add(new Product {Id = 3, Name = "Monitor", Inventory = 10, Price = 67 });
            _context.Products.Add(new Product {Id = 4, Name = "CPU", Inventory = 10, Price = 2000 });

            _context.SaveChanges();
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}