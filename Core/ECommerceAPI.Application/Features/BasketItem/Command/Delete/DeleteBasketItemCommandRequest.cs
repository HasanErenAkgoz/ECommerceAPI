using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Command.Delete
{
    public class DeleteBasketItemCommandRequest : IRequest<DeleteBasketItemCommandResponse> { }
}
