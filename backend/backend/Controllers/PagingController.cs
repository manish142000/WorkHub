using backend.Data;
using backend.Logging;
using backend.Models;
using backend.Models.Responses;
using backend.Repository.Irepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Controllers
{
    [Route("api/order/paging")]
    [ApiController]
    public class PagingController : ControllerBase
    {
        private readonly IOrderRepository _order;

        private readonly PagingResponse _response; 

        private readonly Ilogging _logger;

        public PagingController(
                IOrderRepository order,
                Ilogging logger
        )
        {
            _order = order;
            _response = new PagingResponse();
            _logger = logger;
        }

        [Authorize]
        [Route("{pageNo:int}")]
        [HttpGet]
        public async Task<ActionResult<PagingResponse>> GetOrders( int pageNo ) {

            //_logger.Log(pageNo.ToString(), "");

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(accessToken);

            string email = decodedValue.Claims.First(claim => claim.Type == "Email").Value;
            // Per page 7 entries 
            // sort the data in ascending order of Date 
            IEnumerable<Order> list = await _order.ByOrder(true, email);

            IEnumerable<PagingData> pagingEnumerable = list.Select(x => new PagingData(x.Breakfast, x.Lunch, x.DayCreated, x.DateCreated));

            int size = pagingEnumerable.Count();

            int start = 7 * pageNo;

            if( start >= size )
            {
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return _response;
            }
            
            int pageEntries = size - start;

            List<PagingData> PagingList = new List<PagingData>(pagingEnumerable);

            PagingList = PagingList.GetRange(start, Math.Min(Math.Min(size, 7), pageEntries));

            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.pagingData = PagingList;

            return _response;
        }
    }
}
