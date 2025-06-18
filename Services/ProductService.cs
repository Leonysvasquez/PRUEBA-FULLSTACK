using ProductSales.Models;
using ProductSales.Repositories;

namespace ProductSales.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Product?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Product> CreateAsync(Product product) => _repository.CreateAsync(product);
        public Task<Product?> UpdateAsync(Product product) => _repository.UpdateAsync(product);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
