using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
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
  

        public ProductsController(IProductWriteRepository productWriteRepository,IProductReadRepository productReadRepository
            , IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
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
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            

            Random random = new Random();
            foreach (IFormFile File in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{random.Next()}{Path.GetExtension(File.Name)}");
                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,1024*1024,useAsync : false);
                await File.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

            }
            return Ok();
        }
    }
}
