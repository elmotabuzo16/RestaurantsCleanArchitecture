using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.Atleast20)]
        public async Task<IActionResult> GetAllDishesForRestaurant([FromRoute] int restaurantId)
        {
            var dish = await _mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));

            return Ok(dish);
        }

        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetDishByIdForRestaurant([FromRoute]int restaurantId, [FromRoute]int dishId)
        {
            var dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));

            return Ok(dish);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetDishByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDish([FromRoute] int restaurantId)
        {
            await _mediator.Send(new DeleteAllDishCommand(restaurantId));

            return NoContent();
        }

    }
}
