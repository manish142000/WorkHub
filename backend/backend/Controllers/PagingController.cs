using backend.Data;
using backend.Data.URI;
using backend.Logging;
using backend.Models;
using backend.Models.Responses;
using backend.Repository.Irepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

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
        //[Route("{pageSize:int}/{pageNo}/{startDate}/{endDate}")]
        [HttpGet]
        public async Task<ActionResult<PagingResponse>> GetOrders([FromQuery] pagingUri obj) {


            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(accessToken);

            string email = decodedValue.Claims.First(claim => claim.Type == "Email").Value;

            _logger.Log("Ye start date hai " + obj.startDate, "");

            _logger.Log("Ye end date hai " + obj.endDate, "");

            // sort the data in ascending order of Date 
            IEnumerable<Order> list = await _order.ByOrder(true, email);
            if( list.Count() > 0)
            {
                _logger.Log("1st passed", "");
            }
            if( list.Any() && !string.IsNullOrEmpty(obj.startDate) )
            {
                list = await _order.ByStartDate(list, obj.startDate);
            }
            if (list.Count() > 0)
            {
                _logger.Log("2nd passed", "");
            }
            if (list.Any() && !string.IsNullOrEmpty(obj.endDate))
            {
                list = await _order.ByEndDate(list, obj.endDate);
            }
            if (list.Count() > 0)
            {
                _logger.Log("3rd passed", "");
            }

            IEnumerable<PagingData> pagingEnumerable = list.Select(x => new PagingData(x.Breakfast, x.Lunch, x.DayCreated, x.DateCreated));

            int size = pagingEnumerable.Count();

            int start = obj.pageSize * obj.pageNo;

            if( start >= size )
            {
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return _response;
            }
            
            int pageEntries = size - start;

            List<PagingData> PagingList = new List<PagingData>(pagingEnumerable);

            PagingList = PagingList.GetRange(start, Math.Min(Math.Min(size, obj.pageSize), pageEntries));

            if( PagingList.Count() > 0)
            {
                _logger.Log("4th passed", "");
            }
            _logger.Log("5th passed", "");
            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.pagingData = PagingList;
            _response.pagingLength = size;


            return _response;
        }

    }
}
