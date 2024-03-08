using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetAllProductQueryRequestHandler> _logger;
        public GetAllProductQueryRequestHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryRequestHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
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
            _logger.LogInformation("Get All Product");
            return new()
            {
                Products = products,
                TotalCount = totalCount,
            };
        }
    }
}
