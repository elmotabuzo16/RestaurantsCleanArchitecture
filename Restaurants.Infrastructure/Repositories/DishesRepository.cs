using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository : IDishesRepository
    {
        private RestaurantsDbContext _context { get; }

        public DishesRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Dish entity)
        {
            await _context.Dishes.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
