using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantsRepository
    {
        private readonly RestaurantsDbContext _context;

        public RestaurantRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.Include(r => r.Dishes).ToListAsync();
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = _context
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                   || r.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), x => x.Name },
                    { nameof(Restaurant.Description), x => x.Description },
                    { nameof(Restaurant.Category), x => x.Category },
                };

                var selectedColumn = columnSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }

        public async Task<Restaurant?> GetById(int Id)
        {
            return await _context.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> CreateRestaurant(Restaurant entity)
        {
            await _context.Restaurants.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteRestauraunt(Restaurant entity)
        {
            _context.Restaurants.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRestauraunt(Restaurant entity)
        {
            _context.Restaurants.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
