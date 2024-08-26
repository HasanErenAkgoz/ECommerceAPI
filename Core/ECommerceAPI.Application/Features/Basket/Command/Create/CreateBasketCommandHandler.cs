using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Command.Create
{
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommandRequest, CreateBasketCommandResponse>
    {

        private readonly IBasketService _basketService;

        public CreateBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<CreateBasketCommandResponse> Handle(CreateBasketCommandRequest request, CancellationToken cancellationToken)
        {
           await _basketService.AddItemToBasketAsync(new()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            });

            return new();
        }
    }
}
