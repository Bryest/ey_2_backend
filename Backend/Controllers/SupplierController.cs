using Backend.Model;
using Backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController:ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _supplierRepository.GetSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSupplier(Guid id)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
            if(supplier == null) 
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier (Supplier supplier)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            supplier.Id=Guid.NewGuid();
            await _supplierRepository.AddSupplierAsync(supplier);
            return Ok(new { supplier, Message = "Supplier created succesfully"});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier(Guid id,Supplier supplier)
        {
            if(id != supplier.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            await _supplierRepository.UpdateSupplierAsync(supplier);
            return Ok(new {supplier.Id, Message = "Supplier updated succesfully"});
        }

        [HttpDelete]
        public async Task<IActionResult>RemoveSupplier(Guid id)
        {
            await _supplierRepository.DeleteSupplierAsync(id);
            return Ok(new {Message= $"Supplier with ${id} removed succesfully"});
        }
    }
}
