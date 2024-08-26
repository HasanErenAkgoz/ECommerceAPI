using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Command.Delete
{
    public class DeleteBasketCommandRequest : IRequest<DeleteBasketCommandResponse>
    {
        public string BasketItemId { get; set; }

    }
}
