using ProductSales.Models;

namespace ProductSales.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateSaleAsync(Sale sale);
    }
}
