using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Command.Update
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommandRequest, UpdateBasketCommandResponse>
    {
        private readonly IBasketService _basketService;

        public UpdateBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<UpdateBasketCommandResponse> Handle(UpdateBasketCommandRequest request, CancellationToken cancellationToken)
        {
           await _basketService.UpdateItemToBasketAsync(new()
            {
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity,
            });

            return new();

        }
    }
}
