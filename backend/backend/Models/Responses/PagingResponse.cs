using backend.Data;
using System.Net;

namespace backend.Models.Responses
{
    public class PagingResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;

        public List<string> ErrorsMessages { get; set; }


        public List<PagingData> pagingData { get; set; }
    }
}
