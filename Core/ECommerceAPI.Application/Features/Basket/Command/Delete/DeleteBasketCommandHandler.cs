using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Command.Delete
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommandRequest, DeleteBasketCommandResponse>
    {
        private readonly IBasketService _basketService;

        public DeleteBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommandRequest request, CancellationToken cancellationToken)
        {

            await _basketService.RemoveItemToBasketAsync(request.BasketItemId);
            return new();
        }
    }
}
