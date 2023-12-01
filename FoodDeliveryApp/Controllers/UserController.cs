using FoodDeliveryApp.Entities;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly DBContext _dBContext;

        public UserController(DBContext dBContext)
        {
            _dBContext = dBContext;
            _dBContext.Database.EnsureCreated();
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_dBContext.Users.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _dBContext.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult PostUser(User user)
        {
            IQueryable<User> existingUser = _dBContext.Users.Where(u => u.Email == user.Email);

            if (existingUser.ToList().Count > 0)
            {
                return BadRequest("User Exist");
            }

            user.Password = PasswordHash.CreateHash(user.Password);

            _dBContext.Users.Add(user);
            _dBContext.SaveChanges();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult PutUser(int id,[FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _dBContext.Users.Update(user);
            _dBContext.SaveChanges();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpGet]
        [Route("login")]
        public ActionResult GetLogin([FromQuery] string email, string password)
        {
            IQueryable<User> existingUser = _dBContext.Users.Where(u => u.Email == email);

            if (existingUser.ToList().Count == 0)
            {
                return BadRequest("Invalid credentials");
            }

            if (PasswordHash.Validate(existingUser.ToList()[0].Password, password))
            {
                return Ok("Login success");
            }

            return BadRequest("Invalid credentials");
        }
    }
}
