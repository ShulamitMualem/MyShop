using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entity;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IServices services ;
        public UsersController(IServices myServices)
        {
            services = myServices;
        }
        //GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Shabat", "Shalom" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            
            User user = services.GetUserById(id);
            return user!=null ? Ok(user) : NoContent();
                   

        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            User newUser = services.CreateUser(user);
            if(newUser!=null)
                return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);
            return BadRequest("סיסמתך חלשה מדי");
        }

        [HttpPost("login")]
        public ActionResult<User> LogIn([FromQuery] string userName, string password)
        {
            User user = services.Login(userName, password);
            return user != null ? Ok(user) : NoContent();
        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User userToUpdate)
        {
            services.UpDateUser(id,userToUpdate);//return the result, check if succeeded.
        }
        [HttpPost("password")]
        public IActionResult CheckPassword([FromQuery] string password)
        {
            int score = services.CheckPassword(password);
            return score<3?BadRequest(score):Ok(score);
        }
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
