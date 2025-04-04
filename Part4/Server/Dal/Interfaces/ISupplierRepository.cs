using Dal.Entity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface ISupplierRepository
    {
        Task AddSupplierAsync(SupplierEntity supplier);
       

        Task<SupplierEntity> GetSupplierByIdAsync(int SupplierId);


        //functions for login 
        Task<SupplierEntity> GetSupplierByPhoneAsync(string phone);
        Task<bool> CheckPhoneExistsAsync(string phone);
        //Task<bool> CheckPasswordValidAsync(string email, string password);
        //Task<bool> CheckEmailExistsAsync(string email);
    }
}
