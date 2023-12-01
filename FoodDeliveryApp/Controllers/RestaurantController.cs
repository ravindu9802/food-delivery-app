using FoodDeliveryApp.Entities;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly DBContext _dBContext;

        public RestaurantController(DBContext dBContext)
        {
            _dBContext = dBContext;
            _dBContext.Database.EnsureCreated();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return Ok(_dBContext.Restaurants.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetRestaurant(int id)
        {
            var restaurant = _dBContext.Restaurants.Find(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public ActionResult PostRestaurant(Restaurant restaurant)
        {
            IQueryable<Restaurant> existingRestaurant = _dBContext.Restaurants.Where(r => r.Email == restaurant.Email);

            if (existingRestaurant.ToList().Count > 0)
            {
                return BadRequest("Restaurant Exist");
            }

            _dBContext.Restaurants.Add(restaurant);
            _dBContext.SaveChanges();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        public ActionResult PutRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            //var user = _dBContext.Users.Find(restaurant.UserId);

            //if (user == null)
            //{
            //    return BadRequest("Invalid UserId");
            //}

            _dBContext.Restaurants.Update(restaurant);
            _dBContext.SaveChanges();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        [HttpGet]
        [Route("login")]
        public ActionResult GetLogin([FromQuery] string email, string password)
        {
            IQueryable<Restaurant> existingUser = _dBContext.Restaurants.Where(u => u.Email == email);

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
