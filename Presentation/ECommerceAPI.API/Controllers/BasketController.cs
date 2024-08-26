using ECommerceAPI.Application.Features.Basket.Command.Create;
using ECommerceAPI.Application.Features.Basket.Command.Delete;
using ECommerceAPI.Application.Features.Basket.Command.Update;
using ECommerceAPI.Application.Features.Basket.Query.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBasket([FromQuery]GetAllBasketQueryRequest getAllBasketQueryRequest)
        {
            List<GetAllBasketQueryResponse> response = await _mediator.Send(getAllBasketQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateBasketCommandRequest createBasketCommandRequest)
        {
            CreateBasketCommandResponse response = await _mediator.Send(createBasketCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasket(UpdateBasketCommandRequest updateBasketCommandRequest)
        {
            UpdateBasketCommandResponse response = await _mediator.Send(updateBasketCommandRequest);
            return Ok(response);
        }

        [HttpDelete("BasketItemId")]

        public async Task<IActionResult> DeleteBasket([FromRoute]DeleteBasketCommandRequest deleteBasketCommandRequest)
        {
            DeleteBasketCommandResponse response = await _mediator.Send(deleteBasketCommandRequest);
            return Ok(response);

        }
    }
}
