using Bll.Interfaces;
using Dal.Entities;
using Dal.Entity;
using Dal.Interfaces;
using Dal.Repository;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bll.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderService _orderService;
        public ItemService(IItemRepository itemRepository,IProductRepository productRepository, IOrderService orderService)
        {
            _itemRepository = itemRepository;
            _productRepository = productRepository;
            _orderService = orderService;
        }

        public async Task AddItemAsync(ItemEntity item)
        {
            await _itemRepository.AddItemAsync(item);
        }

        public async Task<string> OnReceiptReceivedAsync(JsonElement data)
        {
            var items = JsonSerializer.Deserialize<Dictionary<string, int>>(data.GetRawText());

            foreach (var item in items)
            {
                ItemEntity myItem = await _itemRepository.GetItemByNameAsync(item.Key);
                if (myItem != null)
                {
                    if (myItem.Amount - item.Value <= myItem.MinimumAmount)
                    {
                        ProductEntity product = await _productRepository.GetCheapestProduct(myItem.Name);
                        if (product != null)
                        {
                            OrderToAddDto orderToAdd = new OrderToAddDto { Quantity = (int)product.MinimumQuantity, ProductId = product.Id };
                            //לבדוק אם כבר קיימת הזמנה שעוד לא אושרה ואם לא אז לבצע הזמנה חדשה 
                            await _orderService.AddOrderAsync(orderToAdd);
                            return "add orders";
                        }
                        else
                        {
                            return "You don't have suppliers who supply the product: " + item.Key;
                        }
                    }
                    myItem.Amount -= item.Value;
                    if(myItem.Amount >= 0)
                        await _itemRepository.UpdateItemAsync(myItem);
                    return "update item: "+ myItem.Name;
                    
                }
                else 
                {
                    return "item not found: "+ item.Key;
                }
            }
            return "";
        }


    }
}
