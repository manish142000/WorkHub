using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;

namespace backend.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;

        public List<string> ErrorsMessages { get; set; }

        public object Result { get; set; }

        public string JwtToken { get; set; }
    }
}
