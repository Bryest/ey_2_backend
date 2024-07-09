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
        {
            await _supplierService.DeleteSupplierAsync(id);
            return Ok(new { Message = $"Supplier with ${id} removed succesfully" });
        }

        [HttpGet("{id}/screening")]
        public async Task<IActionResult> ScreenSupplier(Guid id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var screeningResults = GetHighRiskSuppliers().FirstOrDefault(s=>s.Id==id);

            if (screeningResults == null)
                return Ok(new { Message = "Supplier not found in high-risk list", screeningResults });

            return Ok(new { Message = "Supplier found in high-risk list", screeningResults});
        }

        private List<SupplierHighRisk> GetHighRiskSuppliers()
        {
            return new List<SupplierHighRisk>
            {
                new SupplierHighRisk { Id = new Guid("11A1C681-ACD4-446A-9272-61165DD04FC2"), BusinessName = "Tech Innovators", TaxId = "12345678901" },
                new SupplierHighRisk { Id = new Guid("05C099D4-97A5-4DB3-85F9-F82CDE962C26"), BusinessName = "Green Solutions", TaxId = "12345678901"}
            };
        }
    }
}
