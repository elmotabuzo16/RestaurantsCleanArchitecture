using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;
            
        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants = await _mediator.Send(query);

            return Ok(restaurants);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]int Id)
        {


            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(Id));

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantCommand command)
        {
            int id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int Id)
        {
            await _mediator.Send(new DeleteRestaurantCommand(Id));

            return NoContent();
        }


    }
}
