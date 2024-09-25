using Xunit;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurants.Domain.Entities;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Restaurants.Dtos.Tests
{
    public class RestaurantProfileTests
    {
        private IMapper _mapper;

        public RestaurantProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
            });

            _mapper = configuration.CreateMapper();

        }

        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            // Arrange
            var restaurant = new Restaurant()
            {
                Name = "Test Restaurant",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12-345"
                }
            };

            // Act
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);


            // Assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantToRestaurant_MapsCorrectly()
        {
            // Arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test Restaurant",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12-345",
                
            };

            // Act
            var restaurant = _mapper.Map<Restaurant>(command);


            // Assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);

            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        }


        [Fact()]
        public void CreateMap_ForUpdateRestaurantToRestaurant_MapsCorrectly()
        {
            // Arrange
            var command = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "Test Restaurant",
                Description = "Test Description",
                HasDelivery = true

            };

            // Act
            var restaurant = _mapper.Map<Restaurant>(command);

            // Assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);

        }
    }
}