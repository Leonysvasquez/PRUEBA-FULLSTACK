using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSales.Models;
using ProductSales.Services;

namespace ProductSales.Controllers
{
    [ApiController]
    [Route("api/products")] // 👈 Ruta corregida
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Create(Product product)
        {
            var created = await _service.CreateAsync(product);
            return Ok(new { message = "Producto agregado exitosamente.", product = created });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest(new { message = "El ID del cuerpo no coincide con la ruta." });

            var updated = await _service.UpdateAsync(product);
            if (updated == null)
                return NotFound(new { message = "Producto no encontrado." });

            return Ok(new { message = "Producto actualizado correctamente.", product = updated });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Producto no encontrado para eliminar." });

            return Ok(new { message = "Producto eliminado correctamente." });
        }
    }
}
