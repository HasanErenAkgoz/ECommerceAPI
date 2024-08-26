using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Command.Create
{
    public class CreateBasketCommandRequest : IRequest<CreateBasketCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
