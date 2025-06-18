using ProductSales.DTOs;
using ProductSales.Models;

namespace ProductSales.Services
{
    public interface ISaleService
    {
        Task<Sale> CreateSaleAsync(SaleCreateDto dto);
        Task<List<SaleResponseDto>> GetAllAsync();
        Task<SaleResponseDto?> GetByIdAsync(int id);
        Task<List<SaleResponseDto>> GetByClientIdAsync(int clientId);
        Task<bool> DeleteAsync(int id);
    }
}
