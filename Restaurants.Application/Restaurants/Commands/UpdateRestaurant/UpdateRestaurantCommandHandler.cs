using AutoMapper;
using MediatR;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantsRepository.GetById(request.Id);

            if (restaurant == null)
            {
                return false;
            }
            
            // Get all the properties from request to restaurant entity
            _mapper.Map(request, restaurant);

            await _restaurantsRepository.UpdateRestauraunt(restaurant);
            return true;
        }
    }
}
