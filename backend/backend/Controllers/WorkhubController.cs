using AutoMapper;
using backend.Data;
using backend.Logging;
using backend.Models;
using backend.Models.Dto;
using backend.Repository.Irepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace backend.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class WorkhubController : ControllerBase
    {
        private readonly Ilogging _logger;

        private readonly ApplicationDbContext _db;

        private readonly APIResponse _response;

        private readonly IMapper _mapper;

        private readonly IUserRepository _dbUser;

        public WorkhubController( ApplicationDbContext db, Ilogging logger, IMapper mapper,
            IUserRepository dbuser
            )
        {
            _logger = logger;

            _db = db;

            _response = new APIResponse();

            _mapper = mapper;

            _dbUser = dbuser;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateUserAsync([FromBody] UserCreateDto user)
        {
            try
            {

                if (user == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (await _dbUser.Get(u => u.Email == user.Email) != null)
                {
                    _response.IsSuccess = false;
                    ModelState.AddModelError("CustomError", "User Already exists");
                    return BadRequest();
                }

                if (user.Password != user.ConfirmPassword)
                {
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "The passwords Do not match" };

                    return _response;
                }

                User model = _mapper.Map<User>(user);

                await _dbUser.Create(model);

                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> LoginUser([FromBody] LoginDto credentials)
        {


            User model = await _dbUser.Get(u => u.Email == credentials.Email);

            if (model == null)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { "No such user with Email id exists" };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            if (model.Password != credentials.Password)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { "Invalid Password" };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = credentials.Email;

            return _response;
        }

    }
}
