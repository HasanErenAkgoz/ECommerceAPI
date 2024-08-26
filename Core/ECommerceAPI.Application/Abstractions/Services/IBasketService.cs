using ECommerceAPI.Application.ViewModels.Basket;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemAsync();
        public Task AddItemToBasketAsync(VM_Create_Basket_Item basketItem);
        public Task UpdateItemToBasketAsync(VM_Update_Basket_Item basketItem);
        public Task RemoveItemToBasketAsync(string basketItemId);
    }
}
