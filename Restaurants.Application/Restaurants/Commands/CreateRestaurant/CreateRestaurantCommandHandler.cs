using AutoMapper;
using MediatR;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper, IUserContext userContext)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            var restaurant = _mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser._id;

            int id = await _restaurantsRepository.CreateRestaurant(restaurant);

            return id;
        }
    }
}
