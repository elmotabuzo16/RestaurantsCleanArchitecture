﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteAllDishCommand : IRequest
    {

        public DeleteAllDishCommand(int restaurantId)
        {
            RestaurantId = restaurantId;    
        }

        public int RestaurantId { get; }
    }
}
