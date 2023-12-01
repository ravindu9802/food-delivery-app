using FoodDeliveryApp.Entities;
using FoodDeliveryApp.Models;
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
            var user = _dBContext.Users.Find(restaurant.UserId);

            if (user == null)
            {
                return BadRequest("Invalid UserId");
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

            var user = _dBContext.Users.Find(restaurant.UserId);

            if (user == null)
            {
                return BadRequest("Invalid UserId");
            }

            _dBContext.Restaurants.Update(restaurant);
            _dBContext.SaveChanges();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }
    }
}
