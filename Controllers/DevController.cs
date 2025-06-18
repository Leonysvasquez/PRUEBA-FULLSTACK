using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSales.Data;
using ProductSales.Models;

namespace ProductSales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DevController(AppDbContext context)
        {
            _context = context;
        }

        // 🔁 Borra todos los datos
        [Authorize(Roles = "admin")]
        [HttpDelete("reset")]
        public async Task<IActionResult> Reset()
        {
            _context.SaleItems.RemoveRange(_context.SaleItems);
            _context.Sales.RemoveRange(_context.Sales);
            _context.Clients.RemoveRange(_context.Clients);
            _context.Products.RemoveRange(_context.Products);

            await _context.SaveChangesAsync();
            return Ok(new { message = "Datos eliminados correctamente." });
        }

        // 🧪 Carga datos de prueba
      
        [Authorize(Roles = "admin")]
        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            // Usuarios de prueba
            var admin = new User { Username = "admin", Password = "1234", Role = "admin" };
            var vendedor = new User { Username = "vendedor", Password = "1234", Role = "vendedor" };

            // Agregar si no existen
            if (!_context.Users.Any())
            {
                _context.Users.AddRange(admin, vendedor);
            }

            // Cliente y productos
            var client = new Client { Name = "Laura Mendoza", Email = "laura@mail.com", Phone = "1234567890" };
            var product1 = new Product { Name = "Teclado Mecánico", Description = "RGB, switches azules", Price = 45.99M, Stock = 20 };
            var product2 = new Product { Name = "Mouse Logitech", Description = "Inalámbrico", Price = 29.99M, Stock = 15 };

            _context.Clients.Add(client);
            _context.Products.AddRange(product1, product2);
            await _context.SaveChangesAsync();

            var sale = new Sale
            {
                ClientId = client.Id,
                Date = DateTime.UtcNow,
                Items = new List<SalesItem>
        {
            new SalesItem
            {
                ProductId = product1.Id,
                Quantity = 2,
                Subtotal = product1.Price * 2
            },
            new SalesItem
            {
                ProductId = product2.Id,
                Quantity = 1,
                Subtotal = product2.Price
            }
        },
                Total = (product1.Price * 2) + product2.Price
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Datos de prueba insertados con usuarios, cliente, productos y venta." });
        }



        // 🔍 Verifica estado
        [Authorize]
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new
            {
                clients = _context.Clients.Count(),
                products = _context.Products.Count(),
                sales = _context.Sales.Count()
            });
        }
    }
}
