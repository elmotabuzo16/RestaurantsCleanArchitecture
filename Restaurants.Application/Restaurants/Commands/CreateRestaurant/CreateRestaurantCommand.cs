﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        
        // Address
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
