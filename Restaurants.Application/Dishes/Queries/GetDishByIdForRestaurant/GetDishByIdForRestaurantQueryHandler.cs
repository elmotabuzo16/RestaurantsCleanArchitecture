using AutoMapper;
using MediatR;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        private readonly IDishesRepository _dishesRepository;

        public GetDishByIdForRestaurantQueryHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper, IDishesRepository dishesRepository)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _dishesRepository = dishesRepository;
        }
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantsRepository.GetById(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == request.DishId)
                ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            var dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;
        }
    }
}
