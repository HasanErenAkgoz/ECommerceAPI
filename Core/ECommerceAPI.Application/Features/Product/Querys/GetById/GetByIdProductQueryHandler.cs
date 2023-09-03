using ECommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = ECommerceAPI.Domain.Entities;
namespace ECommerceAPI.Application.Features.Product.Querys.GetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryReqeust, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository =  productReadRepository;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryReqeust request, CancellationToken cancellationToken)
        {
            Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Product = product,
            };

        }
    }
}
