using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entity;
using Services.UserService;
using DTO;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MyShop;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IMyServices services;
        IMapper _mapper;
        ILogger<UsersController> _logger;
        IMemoryCache _cache;

        private readonly JwtTokenHelper _jwtTokenHelper;

        public UsersController(IMyServices myServices, IMapper mapper, ILogger<UsersController> logger, IMemoryCache cache, IConfiguration configuration)
        {
            services = myServices;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
            _jwtTokenHelper = new JwtTokenHelper(configuration);
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetById>> Get(int id)
        {
            if (!_cache.TryGetValue("user", out User user))
            {
                user = await services.GetUserById(id);
            }
            UserGetById userGetById = _mapper.Map<User, UserGetById>(user);
            return userGetById != null ? Ok(userGetById) : NoContent();


        }

        // POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUser user)
        {
            User new_User = _mapper.Map<CreateUser, User>(user);
            User newUser = await services.CreateUser(new_User);
            if (newUser != null)
                return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);
            return BadRequest("סיסמתך חלשה מדי");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LogIn([FromQuery] string userName, string password)
        {
            _logger.LogInformation($"Login attempted with user name: {userName} and password: {password}");
            User user = await services.Login(userName, password);
            if (user != null)
            {
                var token = _jwtTokenHelper.GenerateToken(user);
                Response.Cookies.Append("jwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });
                return Ok(new { user });
            }
            return NoContent();
        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] CreateUser userToUpdate)
        {
            User user = _mapper.Map<CreateUser, User>(userToUpdate);
            return Ok(await services.UpDateUser(id, user));
        }
        [HttpPost("password")]
        public IActionResult CheckPassword([FromQuery] string password)
        {
            int score = services.CheckPassword(password);
            return score < 3 ? BadRequest(score) : Ok(score);
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");
            return Ok();
        }

    }
}
