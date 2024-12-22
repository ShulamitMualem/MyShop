using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entity;
using Services.UserService;
using DTO;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IMyServices services ;
        IMapper _mapper;
        public UsersController(IMyServices myServices, IMapper mapper)
        {
            services = myServices;
            _mapper = mapper;
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetById>> Get(int id)
        {
           
            User user =await services.GetUserById(id);
            UserGetById userGetById = _mapper.Map<User, UserGetById>(user);
            return userGetById != null ? Ok(userGetById) : NoContent();
                   

        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUser user)
        {
            User new_User = _mapper.Map<CreateUser, User>(user);
            User newUser =await services.CreateUser(new_User);
            if(newUser!=null)
                return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);
            return BadRequest("סיסמתך חלשה מדי");
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LogIn([FromQuery] string userName, string password)
        {
            User user = await services.Login(userName, password);
            return user != null ? Ok(user) : NoContent();
        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CreateUser userToUpdate)
        {
            User user = _mapper.Map<CreateUser, User>(userToUpdate);
           await services.UpDateUser(id,user);
        }
        [HttpPost("password")]
        public IActionResult CheckPassword([FromQuery] string password)
        {
            int score = services.CheckPassword(password);
            return score<3?BadRequest(score):Ok(score);
        }

    }
}
