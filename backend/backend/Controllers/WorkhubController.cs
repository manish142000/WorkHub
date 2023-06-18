using backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkhubController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly ApplicationDbContext _db;

        public WorkhubController( ApplicationDbContext db, ILogger logger )
        {
            _logger = logger;

            _db = db;
        }


    }
}
