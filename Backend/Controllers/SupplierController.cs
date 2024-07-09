using Backend.Dto;
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
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto newSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                BusinessName= newSupplier.BusinessName,
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

        [HttpPut]
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

        [HttpDelete]
        public async Task<IActionResult> RemoveSupplier(Guid id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return Ok(new { Message = $"Supplier with ${id} removed succesfully" });
        }

        [HttpGet("{id}/screening")]
        public async Task<IActionResult> ScreenSupplier(Guid id, [FromQuery] string[] sources)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var screeningResults = await PerformScreeningAsync(supplier, sources);

            return Ok(screeningResults);
        }

        private async Task<ScreeningResult> PerformScreeningAsync(Supplier supplier, string[] sources)
        {
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
