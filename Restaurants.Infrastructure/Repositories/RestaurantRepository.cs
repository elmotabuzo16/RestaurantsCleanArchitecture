using Microsoft.EntityFrameworkCore;
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
    public class RestaurantRepository : IRestaurantsRepository
    {
        private RestaurantsDbContext _context { get; }

        public RestaurantRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetById(int Id)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
