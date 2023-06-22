using AutoMapper;
using backend.Logging;
using backend.Models;
using backend.Models.Dto;
using backend.Repository.Irepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Ilogging _logger;

        private readonly APIResponse _response;

        private readonly IMapper _mapper;

        private readonly IOrderRepository _dbOrder;

        public OrderController(
            Ilogging logger,
            IOrderRepository dbOrder,
            IMapper mapper
            )
        {
            _logger = logger;
            _dbOrder = dbOrder;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] OrderDto order)
        {
            
            if( order == null)
            {
                _response.StatusCode = HttpStatusCode.NoContent;
                return _response;
            }

            if( await _dbOrder.Get( u => ( u.UserEmail == order.UserEmail) ) == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorsMessages= new List<string>() { "No user with such email exists" };
                return _response;
            }

            Order model = _mapper.Map<Order>(order);

            await _dbOrder.Create(model);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = model;
            return _response;
        }

    }
}
