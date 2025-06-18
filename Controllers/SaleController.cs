using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSales.DTOs;
using ProductSales.Services;

namespace ProductSales.Controllers
{
    [ApiController]
    [Route("api/sales")] // 👈 Ruta corregida y clara
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [Authorize(Roles = "admin,vendedor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var sale = await _saleService.CreateSaleAsync(dto);
                return Ok(new { message = "Venta registrada exitosamente.", sale });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            return sale == null ? NotFound() : Ok(sale);
        }

        [HttpGet("client/{id}")]
        public async Task<IActionResult> GetByClient(int id)
        {
            var sales = await _saleService.GetByClientIdAsync(id);
            return Ok(new { count = sales.Count, sales });
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
