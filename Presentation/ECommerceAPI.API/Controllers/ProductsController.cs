using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Product.Commands.Create;
using ECommerceAPI.Application.Features.Product.Commands.Delete;
using ECommerceAPI.Application.Features.Product.Commands.Update;
using ECommerceAPI.Application.Features.Product.Querys.GetAll;
using ECommerceAPI.Application.Features.Product.Querys.GetById;
using ECommerceAPI.Application.Features.ProductImage.Commands.ChangeShowCaseImage;
using ECommerceAPI.Application.Features.ProductImage.Commands.DeleteProductImage;
using ECommerceAPI.Application.Features.ProductImage.Commands.UpdateById;
using ECommerceAPI.Application.Features.ProductImage.Querys.GetProductImages;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        readonly private IConfiguration _configuration;
        readonly private IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository
            , IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IProductImageFileWriteRepository productImageFileWriteRepository,
            IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IInvoiceFileWriteRepository ınvoiceFileWriteRepository,
            IInvoiceFileReadRepository ınvoiceFileReadRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
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
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryReqeust getByIdProductQueryReqeust)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryReqeust);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return Ok((int)HttpStatusCode.Created);
        }

        [Authorize(AuthenticationSchemes = "Admin")]

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok((int)HttpStatusCode.Accepted);
        }

        [Authorize(AuthenticationSchemes = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
            return Ok((int)HttpStatusCode.Accepted);
        }

        [Authorize(AuthenticationSchemes = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductCommandRequest uploadProductCommandRequest)
        {
            uploadProductCommandRequest.Files = Request.Form.Files;
            UploadProductCommandResponse response = await _mediator.Send(uploadProductCommandRequest);
            return Ok((int)HttpStatusCode.Accepted);
        }

        [Authorize(AuthenticationSchemes = "Admin")]
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Admin")]
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            DeleteProductImageCommandResponse response = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok();
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowCase([FromQuery] ChangeShowCaseImageCommandRequest changeShowCaseImageCommandRequest)
        {
            ChangeShowCaseImageCommandResponse response = await _mediator.Send(changeShowCaseImageCommandRequest);
            return Ok(response); ;
        }

    }
}
