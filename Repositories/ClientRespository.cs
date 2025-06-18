using Microsoft.EntityFrameworkCore;
using ProductSales.Data;
using ProductSales.Models;

namespace ProductSales.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> CreateAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client?> UpdateAsync(Client updatedClient)
        {
            var existingClient = await _context.Clients.FindAsync(updatedClient.Id);
            if (existingClient == null)
            {
                return null;
            }

            existingClient.Name = updatedClient.Name;
            existingClient.Email = updatedClient.Email;
            existingClient.Phone = updatedClient.Phone;

            await _context.SaveChangesAsync();

            return existingClient;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
