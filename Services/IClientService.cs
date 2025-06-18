using ProductSales.DTOs;
using ProductSales.Models;

namespace ProductSales.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
        Task<Client?> UpdateAsync(ClientDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
