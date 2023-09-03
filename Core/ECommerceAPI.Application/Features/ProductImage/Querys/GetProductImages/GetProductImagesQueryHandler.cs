using ECommerceAPI.Application.Features.Product.Querys.GetById;
using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.ProductImage.Querys.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IConfiguration _configuration;
        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository,IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        async Task<List<GetProductImagesQueryResponse>> IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>.Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Entites.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                          .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            return (product.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                Path = $"{_configuration["BaseStoregeUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id
            }).ToList());
        }
    }
}
