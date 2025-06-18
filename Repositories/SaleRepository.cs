using Microsoft.EntityFrameworkCore;
using ProductSales.Data;
using ProductSales.Models;

namespace ProductSales.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            _context.Sales.Add(sale);

            // Actualizar stock de los productos vendidos
            foreach (var item in sale.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                }
            }

            await _context.SaveChangesAsync();
            return sale;
        }
    }
}
