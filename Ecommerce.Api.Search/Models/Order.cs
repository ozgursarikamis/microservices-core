using System;
using System.Collections.Generic;

namespace Ecommerce.Api.Search.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    { 
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
