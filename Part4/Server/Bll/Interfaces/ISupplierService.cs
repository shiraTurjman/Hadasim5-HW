using Dal.Dto;
using Dal.Entity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface ISupplierService
    {
        Task AddSupplierAsync(SupplierToAddDto supplier);
       
        Task<SupplierEntity> GetSupplierByIdAsync(int id);


        //functions for login 
        //Task<UserEntity> GetUserByEmailAsync(string email);
        //Task<bool> CheckPasswordValidAsync(string email, string password);
        //Task<bool> CheckEmailExistsAsync(string email);

         Task<SupplierDto> LoginAsync(string phone);
    }
}
