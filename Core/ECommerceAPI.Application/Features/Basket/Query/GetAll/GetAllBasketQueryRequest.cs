using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Query.GetAll
{
    public class GetAllBasketQueryRequest : IRequest<List<GetAllBasketQueryResponse>> { }
}
