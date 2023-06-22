using AutoMapper;
using backend.Logging;
using backend.Models;
using backend.Models.Dto;
using backend.Repository.Irepository;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IUserRepository _dbUser;
        public OrderController(
            Ilogging logger,
            IOrderRepository dbOrder,
            IMapper mapper,
            IUserRepository dbUser 
            )
        {
            _logger = logger;
            _dbOrder = dbOrder;
            _mapper = mapper;
            _response = new APIResponse();
            _dbUser = dbUser;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] OrderDto order)
        {
            _logger.Log("Yaha aa rha !", "");
            if( order == null)
            {
                _response.StatusCode = HttpStatusCode.NoContent;
                return _response;
            }

            if( await _dbUser.Get( u => ( u.Email == order.UserEmail) ) == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorsMessages= new List<string>() { "No user with such email exists" };
                return _response;
            }
            _logger.Log("Database mai changes ho rha! ", " ");
            Order model = _mapper.Map<Order>(order);

            await _dbOrder.Create(model);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = order;
            return _response;
        }

    }
}
