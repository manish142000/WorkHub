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
        public async Task<ActionResult<APIResponse>> CreateUserAsync([FromBody] UserCreateDto user )
        {
            try
            {

                if (user == null)
                {
                    _logger.Log("user null hai", "Error");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (await _dbUser.Get(u => u.Name == user.Name) != null)
                {
                    _response.IsSuccess = false;
                    ModelState.AddModelError("CustomError", "User Already exists");
                    return BadRequest();
                }

                if( user.Password != user.ConfirmPassword)
                {
                    _response.IsSuccess = false;
                    _response.ErrorsMessages = new List<string>() { "The passwords Do not match" };

                    return _response;
                }

                _logger.Log("Yaha aa rha 1", "");

                User model = _mapper.Map<User>(user);

                _logger.Log("Yaha aa rha 2", "");

                await _dbUser.Create(model);

                _logger.Log("Yaha aa rha 3", "");

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

    }
}
