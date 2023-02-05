using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;

        readonly private IProductReadRepository _productReadRepository;
        readonly private IOrderReadRepository _orderReadRepository;
        readonly private ICustomerReadRepository _customerReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository,
            ICustomerWriteRepository customerWriteRepository, IProductReadRepository productReadRepository,
            IOrderReadRepository orderReadRepository, ICustomerReadRepository customerReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _productReadRepository = productReadRepository;
            _orderReadRepository = orderReadRepository;
            _customerReadRepository = customerReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {

            Order order = await _orderReadRepository.GetByIdAsync("c58560a5-1964-408d-a280-996d851854d4");
            order.Adress = "İstanbul/Ümraniye";
            await _orderWriteRepository.SaveAsync();
            
        }
    }
}
