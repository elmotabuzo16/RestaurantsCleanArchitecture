using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDto>>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }
        public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var (restaurants, totalCount) = await _restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize, request.PageNumber, request.SortBy, request.SortDirection);
            var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            var result = new PageResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
