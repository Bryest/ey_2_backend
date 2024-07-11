using Backend.Dto;
using Backend.Model;
using Backend.Repository;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierService supplierService, HttpClient httpClient, ILogger<SupplierController> logger)
        {
            _supplierService = supplierService;
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByOrderLastEditedSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersByOrderLastEditedAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSupplier(Guid id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto newSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                BusinessName = newSupplier.BusinessName,
                TradeName = newSupplier.TradeName,
                TaxId = newSupplier.PhoneNumber,
                PhoneNumber = newSupplier.PhoneNumber,
                Email = newSupplier.Email,
                Website = newSupplier.Website,
                PhysicalAddress = newSupplier.PhysicalAddress,
                Country = newSupplier.Country,
                AnnualBilling = newSupplier.AnnualBilling,
                LastEdited = DateTime.Now
            };

            await _supplierService.AddSupplierAsync(supplier);
            return Ok(new { supplier, Message = "Supplier created succesfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] SupplierDto supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var existingSupplier = await _supplierService.GetSupplierByIdAsync(id);

            if (existingSupplier == null)
            {
                return NotFound();
            }

            existingSupplier.BusinessName = supplier.BusinessName;
            existingSupplier.TradeName = supplier.TradeName;
            existingSupplier.TaxId = supplier.TaxId;
            existingSupplier.PhoneNumber = supplier.PhoneNumber;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Website = supplier.Website;
            existingSupplier.PhysicalAddress = supplier.PhysicalAddress;
            existingSupplier.Country = supplier.Country;
            existingSupplier.AnnualBilling = supplier.AnnualBilling;

            existingSupplier.LastEdited = DateTime.UtcNow;

            await _supplierService.UpdateSupplierAsync(existingSupplier);
            return Ok(new { existingSupplier.Id, Message = "Supplier updated succesfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSupplier(Guid id)
        {http://127.0.0.1:5000/search?entity_name=a
            await _supplierService.DeleteSupplierAsync(id);
            return Ok(new { Message = $"Supplier with ${id} removed succesfully" });
        }

    
        [HttpGet("screening/{entity_name}")]
        public async Task<IActionResult> GetEntityData(string entity_name)
        {
            string url = $"http://127.0.0.1:5000/search?entity_name={entity_name}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);

                if (string.IsNullOrEmpty(response))
                {
                    return NotFound($"No data found for the specified entity at {url}.");
                }

                return Content(response, "application/json");
            }
            catch (HttpRequestException)
            {
                return StatusCode(500, "Error fetching data from the external service.");
            }
        }
    }
}


