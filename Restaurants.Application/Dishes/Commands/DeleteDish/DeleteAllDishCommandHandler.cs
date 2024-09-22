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

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteAllDishCommandHandler : IRequestHandler<DeleteAllDishCommand>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        private readonly IDishesRepository _dishesRepository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

        public DeleteAllDishCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper, IDishesRepository dishesRepository, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _dishesRepository = dishesRepository;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }
        public async Task Handle(DeleteAllDishCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantsRepository.GetById(request.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                throw new ForbidException();
            }


            await _dishesRepository.DeleteAll(restaurant.Dishes);
        }
    }
}
