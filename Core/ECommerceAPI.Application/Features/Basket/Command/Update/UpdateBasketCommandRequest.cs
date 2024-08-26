using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Command.Update
{
    public class UpdateBasketCommandRequest : IRequest<UpdateBasketCommandResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
