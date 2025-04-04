using Bll.Interfaces;
using Dal.Dto;
using Dal.Entity;
using Dal.Interfaces;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IOrderRepository _orderRepository;
        public SupplierService(ISupplierRepository supplierRepository, IOrderRepository orderRepository)
        {
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
        }

        public async Task AddSupplierAsync(SupplierToAddDto supplier)
        {
            try
            {
                var supplerToAdd = new SupplierEntity
                {
                    Name = supplier.Name,
                    Phone = supplier.Phone,
                    Agent = supplier.Agent
                };
                await _supplierRepository.AddSupplierAsync(supplerToAdd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<SupplierEntity> GetSupplierByIdAsync(int id)
        {
            return await _supplierRepository.GetSupplierByIdAsync(id);
        }

        

        public async Task<SupplierDto> LoginAsync(string phone)
        {
            if (await _supplierRepository.CheckPhoneExistsAsync(phone))
            {
                var supplier = await _supplierRepository.GetSupplierByPhoneAsync(phone);
                var orders = await _orderRepository.GetOrderBySupplierAsync(supplier.Id);
                var supplerDto = new SupplierDto
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Agent = supplier.Agent,
                    Phone = phone,
                    //Orders = orders
                };
                return supplerDto;

            }
            else
            {
                throw new Exception("Phone not valid.");
            }
        }
    }
}
