using MediatR;

namespace ECommerceAPI.Application.Features.Basket.Query.GetById
{
    public class GetByIdBasketQueryHandler : IRequestHandler<GetByIdBasketQueryRequest, GetByIdBasketQueryResponse>
    {
        public Task<GetByIdBasketQueryResponse> Handle(GetByIdBasketQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
