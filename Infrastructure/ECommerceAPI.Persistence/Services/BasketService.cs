using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Basket;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketReadRepository basketReadRepository, IBasketItemReadRepository basketItemReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketReadRepository = basketReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
        }

        private async Task<Basket?> ContextUser()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await _userManager.Users
                         .Include(u => u.Baskets)
                         .FirstOrDefaultAsync(u => u.UserName == username);

                var _basket = from basket in user.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = order
                              };

                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }

                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hatayla karşılaşıldı...");
        }


        public async Task AddItemToBasketAsync(VM_Create_Basket_Item basketItem)
        {
            Basket? basket = await ContextUser();

            if (basket != null)
            {
                BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(p => p.BasketId == basket.Id && p.ProductId == Guid.Parse(basketItem.ProductId));

                if (_basketItem != null)
                    _basketItem.Quentity++;
                else
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quentity = basketItem.Quantity
                    });

                await _basketItemWriteRepository.SaveAsync();
            }

        }

        public async Task<List<BasketItem>> GetBasketItemAsync()
        {
            Basket? basket = await ContextUser();

            Basket? result = await _basketReadRepository.Table.Include(b => b.BasketItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(b => b.Id == basket.Id);
            return result.BasketItems.ToList();

        }

        public async Task RemoveItemToBasketAsync(string basketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);

            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateItemToBasketAsync(VM_Update_Basket_Item basketItem)
        {
            BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);

            if (_basketItem != null)
            {
                _basketItem.Quentity = basketItem.Quantity;
                _basketItemWriteRepository.Update(_basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
