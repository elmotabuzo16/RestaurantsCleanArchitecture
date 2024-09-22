using Restaurants.Application.Users;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;


namespace Restaurants.Infrastructure.Authorization.Services
{ 
    public class RestaurantAuthorizationService : IRestaurantAuthorizationService
    {
        private readonly IUserContext _userContext;

        public RestaurantAuthorizationService(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public bool Authorize(Restaurant restaurant, Domain.Constants.ResourceOperation resourceOperation)
        {
            var user = _userContext.GetCurrentUser();

            if (resourceOperation == Domain.Constants.ResourceOperation.Create || resourceOperation == Domain.Constants.ResourceOperation.Read)
            {
                // log Create/Read operation - successful authorization
                return true;
            }

            if (resourceOperation == Domain.Constants.ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                return true;
            }

            if (resourceOperation == Domain.Constants.ResourceOperation.Delete || resourceOperation == Domain.Constants.ResourceOperation.Update && user.IsInRole(UserRoles.Owner))
            {
                return true;
            }

            return false;
        }
    }
}
