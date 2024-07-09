using Backend.Model;
using Backend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<IEnumerable<Supplier>> GetAllSuppliersByOrderLastEditedAsync()
        {
            return await _supplierRepository.GetAllSuppliersByOrderLastEditedAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(Guid id)
        {
            return await _supplierRepository.GetSupplierByIdAsync(id);
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            try
            {
                await _supplierRepository.AddSupplierAsync(supplier);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while Create supplier: {ex.Message}");
            }
        }
        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            try
            {
                await _supplierRepository.UpdateSupplierAsync(supplier);
                
            }catch (Exception ex)
            {
                throw new Exception($"An error ocurred while Update supplier: {ex.Message}");
            }
        }

        public async Task DeleteSupplierAsync(Guid id)
        {
            try
            {
                await _supplierRepository.DeleteSupplierAsync(id);

            }catch( Exception ex)
            {
                throw new Exception($"An error ocurred while Delete Supplier: {ex.Message}");
            }
        }
    }
}
