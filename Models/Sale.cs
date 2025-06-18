using System;
using System.Collections.Generic;

namespace ProductSales.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public decimal Total { get; set; }
        public List<SalesItem> Items { get; set; } = new();
    }
}
