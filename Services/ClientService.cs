using ProductSales.DTOs;
using ProductSales.Models;
using ProductSales.Repositories;
using ProductSales.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repo;

    public ClientService(IClientRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task<Client?> GetByIdAsync(int id)
        => await _repo.GetByIdAsync(id);

    public async Task<Client> CreateAsync(Client client)
        => await _repo.CreateAsync(client);

    public async Task<Client?> UpdateAsync(ClientDto dto)
    {
        var existingClient = await _repo.GetByIdAsync(dto.Id);
        if (existingClient == null)
            return null;

        existingClient.Name = dto.Name;
        existingClient.Email = dto.Email;
        existingClient.Phone = dto.Phone;

        return await _repo.UpdateAsync(existingClient);
    }

    public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);
}
