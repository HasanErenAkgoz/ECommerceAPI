using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entitys = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Product.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Entitys.Product productToUpdate = await _productReadRepository.GetByIdAsync(request.Product.Id);
            productToUpdate.Stock = request.Product.Stock;
            productToUpdate.Price = request.Product.Price;
            productToUpdate.Name = request.Product.Name;
            productToUpdate.UpdateDate = DateTime.Now;
            await _productWriteRepository.SaveAsync();
            _logger.LogInformation("Updated Product");
            return new();
        }
    }
}
