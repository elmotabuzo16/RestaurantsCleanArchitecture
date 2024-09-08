using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetById(int Id);
        Task<int> CreateRestaurant(Restaurant entity);
        Task UpdateRestauraunt(Restaurant entity);
        Task DeleteRestauraunt(Restaurant entity);
    }
}
