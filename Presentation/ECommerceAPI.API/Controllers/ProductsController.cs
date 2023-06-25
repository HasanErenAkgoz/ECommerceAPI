using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.File;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IWebHostEnvironment _webHostEnvironment;
        readonly private IFileWriteRepository _fileWriteRepository;
        readonly private IFileReadRepository _fileReadRepository;
        readonly private IProductImageFileReadRepository _productImageFileReadRepository;
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly private IInvoiceFileReadRepository _ınvoiceFileReadRepository;
        readonly private IInvoiceFileWriteRepository _ınvoiceFileWriteRepository;
        readonly private IStorageService _storageService;

        public ProductsController(IProductWriteRepository productWriteRepository,IProductReadRepository productReadRepository
            , IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IProductImageFileWriteRepository productImageFileWriteRepository,
            IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IInvoiceFileWriteRepository ınvoiceFileWriteRepository,
            IInvoiceFileReadRepository ınvoiceFileReadRepository, IStorageService storageService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _ınvoiceFileWriteRepository = ınvoiceFileWriteRepository;
            _ınvoiceFileReadRepository = ınvoiceFileReadRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination) 
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Size * pagination.Page).Take(pagination.Size);
            return Ok(new
            {
                totalCount,
                products,
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(_productReadRepository.GetByIdAsync(id,false));
        }


        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product product)
        {
            if (ModelState.IsValid)
            {

            }
            await _productWriteRepository.AddAsync(new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CreateDate = DateTime.Now
            });
            await _productWriteRepository.SaveAsync();
            return Ok((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(VM_Update_Product product)
        {
            Product productToUpdate = await _productReadRepository.GetByIdAsync(product.Id);
            productToUpdate.Stock = product.Stock;
            productToUpdate.Price = product.Price;
            productToUpdate.Name = product.Name;
            productToUpdate.UpdateDate = DateTime.Now;
            await _productWriteRepository.SaveAsync();
            return Ok((int)HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok((int)HttpStatusCode.Accepted);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
           var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName

            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return Ok((int)HttpStatusCode.Accepted);
        }
    }
}
