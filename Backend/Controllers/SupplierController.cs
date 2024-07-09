using Backend.Model;
using Backend.Repository;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
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
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            supplier.Id = Guid.NewGuid();
            supplier.LastEdited = DateTime.Now;

            await _supplierService.AddSupplierAsync(supplier);
            return Ok(new { supplier, Message = "Supplier created succesfully" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] Supplier supplier)
        {
            if (id != supplier.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var existingSupplier = await _supplierService.GetSupplierByIdAsync(id);

            if (existingSupplier == null)
            {
                return NotFound();
            }

            existingSupplier.BussinessName = supplier.BussinessName;
            existingSupplier.TradeName = supplier.TradeName;
            existingSupplier.TaxId = supplier.TaxId;
            existingSupplier.PhoneNumber = supplier.PhoneNumber;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Website = supplier.Website;
            existingSupplier.PhysicalAddress = supplier.PhysicalAddress;
            existingSupplier.Country = supplier.Country;
            existingSupplier.AnnualBilling = supplier.AnnualBilling;

            existingSupplier.LastEdited = DateTime.UtcNow;

            await _supplierService.UpdateSupplierAsync(supplier);
            return Ok(new { supplier.Id, Message = "Supplier updated succesfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSupplier(Guid id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return Ok(new { Message = $"Supplier with ${id} removed succesfully" });
        }

        [HttpPost("{id}/screening")]
        public async Task<IActionResult> ScreenSupplier(Guid id, [FromBody] ScreeningRequest screeningRequest)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var screeningResults = await PerformScreeningAsync(supplier, screeningRequest.Sources);

            return Ok(screeningResults);
        }

        private async Task<ScreeningResult> PerformScreeningAsync(Supplier supplier, string[] sources)
        {
            // Implementar la lógica para realizar el cruce con las listas de alto riesgo

            // Ejemplo simulado de resultado de screening
            var screeningResult = new ScreeningResult
            {
                SupplierId = supplier.Id,
                Results = new List<ScreeningMatch>
                {
                    new ScreeningMatch { Source = "OFAC", MatchDetails = "Match found in OFAC list" },
                    new ScreeningMatch { Source = "World Bank", MatchDetails = "Match found in World Bank list" }
                }
            };

            return await Task.FromResult(screeningResult);
        }
    }

    public class ScreeningRequest
    {
        public string[] Sources { get; set; }
    }

    public class ScreeningResult
    {
        public Guid SupplierId { get; set; }
        public List<ScreeningMatch> Results { get; set; }
    }

    public class ScreeningMatch
    {
        public string Source { get; set; }
        public string MatchDetails { get; set; }
    }
}
