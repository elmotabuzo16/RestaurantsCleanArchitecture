using AutoMapper;
using MediatR;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantsRepository.GetById(request.Id);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidException();
            }

            // Get all the properties from request to restaurant entity
            _mapper.Map(request, restaurant);

            await _restaurantsRepository.UpdateRestauraunt(restaurant);
        }
    }
}
