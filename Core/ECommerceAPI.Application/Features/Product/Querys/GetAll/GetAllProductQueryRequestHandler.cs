using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Product.Querys.GetAll
{
    public class GetAllProductQueryRequestHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetAllProductQueryRequestHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            int totalCount = _productReadRepository.GetAll(false).Count();
            List<ProductDTO> products = await _productReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size).Select(
                p => new ProductDTO
                {
                    Price = p.Price,
                    CreateDate = p.CreateDate,
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    UpdateDate = p.UpdateDate
                }).ToListAsync();

            return new()
            {
                Products = products,
                TotalCount = totalCount,
            };
        }
    }
}
