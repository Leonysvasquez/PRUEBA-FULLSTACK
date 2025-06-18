using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSales.DTOs;
using ProductSales.Models;
using ProductSales.Services;

namespace ProductSales.Controllers
{
    [ApiController]
    [Route("api/clients")] // 👈 Ruta limpia y explícita
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _service.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _service.GetByIdAsync(id);
            return client == null ? NotFound() : Ok(client);
        }

        [HttpPost]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Create(Client client)
        {
            var created = await _service.CreateAsync(client);
            return Ok(new { message = "Cliente agregado exitosamente.", client = created });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,vendedor")]
        public async Task<IActionResult> Update(int id, ClientDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "El ID del cuerpo no coincide con la ruta." });

            var updatedClient = await _service.UpdateAsync(dto);
            return updatedClient == null
                ? NotFound(new { message = "Cliente no encontrado." })
                : Ok(new { message = "Cliente actualizado correctamente.", client = updatedClient });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return !deleted
                ? NotFound(new { message = "Cliente no encontrado para eliminar." })
                : Ok(new { message = "Cliente eliminado correctamente." });
        }
    }
}
