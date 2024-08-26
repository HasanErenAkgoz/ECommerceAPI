using MediatR;

namespace ECommerceAPI.Application.Features.BasketItem.Command.Create
{
    public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommandRequest, CreateBasketItemCommanResponse>
    {
        public Task<CreateBasketItemCommanResponse> Handle(CreateBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
