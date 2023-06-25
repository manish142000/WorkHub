using AutoMapper;
using backend.Logging;
using backend.Models;
using backend.Models.Dto;
using backend.Models.Responses;
using backend.Repository.Irepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

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
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(accessToken);

            _logger.Log(decodedValue.Claims.First(claim => claim.Type == "Email").Value, "");

            if ( order == null )
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { "Data is not coming in backend" };
                return _response;
            }

            Order model = _mapper.Map<Order>(order);

            await _dbOrder.Create(model);

            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            _response.ErrorsMessages = new List<string>() { "Order Created" };
            _response.Result = order;

            return _response;
        }

    }
}
