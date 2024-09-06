using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantsService.GetAllRestaurants();

            return Ok(restaurants);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]int Id)
        {
            var restaurant = await _restaurantsService.GetRestaurantById(Id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantDto createRestaurantDto)
        {
            int id = await _restaurantsService.CreateRestaurant(createRestaurantDto);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

    }
}
