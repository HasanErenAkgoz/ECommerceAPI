using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Query.GetAll
{
    public class GetAllBasketQueryHandler : IRequestHandler<GetAllBasketQueryRequest, List<GetAllBasketQueryResponse>>
    {
        private readonly IBasketService _basketService;

        public GetAllBasketQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetAllBasketQueryResponse>> Handle(GetAllBasketQueryRequest request, CancellationToken cancellationToken)
        {

            List<Domain.Entities.BasketItem> basketItems = await _basketService.GetBasketItemAsync();

            return basketItems.Select(p => new GetAllBasketQueryResponse()
            {
                BasketItemId = p.Id.ToString(),
                Name = p.Product.Name.ToUpper(),
                Price = p.Product.Price,
                Quantity = p.Quentity
            }).ToList();
        }
    }
}
