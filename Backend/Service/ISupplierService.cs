using Backend.Model;

namespace Backend.Service
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersByOrderLastEditedAsync();
        Task<Supplier> GetSupplierByIdAsync(Guid id);
        Task AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Guid id);
    }
}
