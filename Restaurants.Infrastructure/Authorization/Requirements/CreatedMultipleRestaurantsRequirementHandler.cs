using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {

        private readonly IUserContext _userContext;
        private readonly IRestaurantsRepository _restaurantsRepository;

        public CreatedMultipleRestaurantsRequirementHandler(IUserContext userContext, IRestaurantsRepository restaurantsRepository)
        {
            _userContext = userContext;
            _restaurantsRepository = restaurantsRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {
            var currentUser = _userContext.GetCurrentUser();

            var restaurants = await _restaurantsRepository.GetAllAsync();

            var userRestaurantCreated = restaurants.Count(r => r.OwnerId == currentUser!._id);

            if (userRestaurantCreated > requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
