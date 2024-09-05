using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private IRestaurantsRepository _restaurantsRepository;

        public RestaurantsService(IRestaurantsRepository restaurantsRepository)
        {
            _restaurantsRepository = restaurantsRepository;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await _restaurantsRepository.GetAllAsync();
        }
    }
}
