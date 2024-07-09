﻿using Backend.Model;

namespace Backend.Repository
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersByOrderLastEditedAsync();
        Task<Supplier> GetSupplierByIdAsync(Guid id);
        Task AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Guid id);
    }
}
