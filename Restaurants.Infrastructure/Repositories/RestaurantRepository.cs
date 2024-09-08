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
            return await _context.Restaurants.Include(r => r.Dishes).ToListAsync();
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
