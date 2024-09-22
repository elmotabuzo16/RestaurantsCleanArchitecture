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

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantsRepository.GetById(request.Id);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                throw new ForbidException();
            }

            await _restaurantsRepository.DeleteRestauraunt(restaurant);
        }
    }
}
