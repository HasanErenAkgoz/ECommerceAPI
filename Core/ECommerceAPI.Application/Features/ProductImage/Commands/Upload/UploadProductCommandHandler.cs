using Azure.Core;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.File;
using MediatR;
using Entitys = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.ProductImage.Commands.UpdateById
{
    public class UploadProductCommandHandler : IRequestHandler<UploadProductCommandRequest, UploadProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IStorageService _storageService;

        public UploadProductCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }

        public async Task<UploadProductCommandResponse> Handle(UploadProductCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string PathOrContainerName)> result = await _storageService.UploadAsync("photo-images",request.Files);

            Entitys.Product? product = await _productReadRepository.GetByIdAsync(request.Id, true);

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.PathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Entitys.Product>() { product }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
