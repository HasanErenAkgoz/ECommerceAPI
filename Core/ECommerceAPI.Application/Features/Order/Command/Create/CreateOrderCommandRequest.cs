using ECommerceAPI.Application.DTOs.Order;
using MediatR;

namespace ECommerceAPI.Application.Features.Order.Command.Create
{
    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        public string Address { get; set; }
        public string Description { get; set; }
    }

}
