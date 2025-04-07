using Dal.Entity;
using Dal.Interfaces;
using Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IDbContextFactory<ServerDbContext> _factory;
        public SupplierRepository(IDbContextFactory<ServerDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<int> AddSupplierAsync(SupplierEntity supplier)
        {
            
            if (supplier == null || string.IsNullOrEmpty(supplier.Phone))
            {
                throw new Exception("Invalid user data.");
            }

            using var context = await _factory.CreateDbContextAsync();
            var existingSupplier = await context.Suppliers.FirstOrDefaultAsync(s => s.Phone == supplier.Phone);
            if (existingSupplier != null)
            {
                throw new Exception("Supplier with this phone already exists.");
            }

            //Console.WriteLine($"Adding user to DB: {supplier.Phone}");

            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();
            return supplier.Id;

        }

        public async Task<bool> CheckPhoneExistsAsync(string phone)
        {
            using var context = _factory.CreateDbContext();
            return await context.Suppliers.AnyAsync(c => c.Phone.Equals(phone));
        }

        //public async Task<bool> CheckPasswordValidAsync(string email, string password)
        //{
        //    using var context = _factory.CreateDbContext();
        //    var user = await context.Users.FirstAsync(c => c.Email.Equals(email));
        //    return user.Password.Equals(password);

        //}
        public async Task<SupplierEntity> GetSupplierByPhoneAsync(string phone)
        {
            using var context = _factory.CreateDbContext();
            var supplier = await context.Suppliers.FirstAsync(u => u.Phone.Equals(phone));
            if (supplier != null)
            {
                return supplier;
            }
            else
                throw new Exception("Could not find supplier with given phone.");
        }
        
        
        public async Task<SupplierEntity> GetSupplierByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var supplier = context.Suppliers.FirstOrDefault(u => u.Id == id);
            if (supplier == null)
            {
                throw new Exception("Could not find user");
            }
            return supplier;
        }

       
        
    }
}
