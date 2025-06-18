using Microsoft.EntityFrameworkCore;
using ProductSales.Data;
using ProductSales.DTOs;
using ProductSales.Models;

namespace ProductSales.Services
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateSaleAsync(SaleCreateDto dto)
        {
            var sale = new Sale
            {
                ClientId = dto.ClientId,
                Date = DateTime.UtcNow,
                Items = new List<SalesItem>(),
                Total = 0
            };

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                {
                    throw new InvalidOperationException($"Producto no disponible o sin stock: ID {item.ProductId}");
                }

                var subtotal = product.Price * item.Quantity;
                sale.Total += subtotal;

                sale.Items.Add(new SalesItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Subtotal = subtotal
                });

                product.Stock -= item.Quantity;
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<List<SaleResponseDto>> GetAllAsync()
        {
            var sales = await _context.Sales
                .Include(s => s.Client)
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .ToListAsync();

            return sales.Select(s => new SaleResponseDto
            {
                Id = s.Id,
                Date = s.Date,
                Total = s.Total,
                Client = new ClientDto
                {
                    Id = s.Client.Id,
                    Name = s.Client.Name,
                    Email = s.Client.Email,
                    Phone = s.Client.Phone
                },
                Items = s.Items.Select(i => new SaleItemResponseDto
                {
                    ProductId = i.Product.Id,
                    ProductName = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    Subtotal = i.Subtotal
                }).ToList()
            }).ToList();
        }


        public async Task<SaleResponseDto?> GetByIdAsync(int id)
        {
            var s = await _context.Sales
                .Include(s => s.Client)
                .Include(s => s.Items).ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (s == null) return null;

            return new SaleResponseDto
            {
                Id = s.Id,
                Date = s.Date,
                Total = s.Total,
                Client = new ClientDto
                {
                    Id = s.Client.Id,
                    Name = s.Client.Name,
                    Email = s.Client.Email,
                    Phone = s.Client.Phone
                },
                Items = s.Items.Select(i => new SaleItemResponseDto
                {
                    ProductId = i.Product.Id,
                    ProductName = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }

        public async Task<List<SaleResponseDto>> GetByClientIdAsync(int clientId)
        {
            var sales = await _context.Sales
                .Where(s => s.ClientId == clientId)
                .Include(s => s.Client)
                .Include(s => s.Items).ThenInclude(i => i.Product)
                .ToListAsync();

            return sales.Select(s => new SaleResponseDto
            {
                Id = s.Id,
                Date = s.Date,
                Total = s.Total,
                Client = new ClientDto
                {
                    Id = s.Client.Id,
                    Name = s.Client.Name,
                    Email = s.Client.Email,
                    Phone = s.Client.Phone
                },
                Items = s.Items.Select(i => new SaleItemResponseDto
                {
                    ProductId = i.Product.Id,
                    ProductName = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    Subtotal = i.Subtotal
                }).ToList()
            }).ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null) return false;

            foreach (var item in sale.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock += item.Quantity;
                }
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
