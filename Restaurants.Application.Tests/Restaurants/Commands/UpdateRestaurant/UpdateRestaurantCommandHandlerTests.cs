using Xunit;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Restaurants.Domain.Repositories;
using AutoMapper;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<IRestaurantsRepository> _restaurantRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthServiceMock;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _restaurantRepoMock = new Mock<IRestaurantsRepository>();
            _mapperMock = new Mock<IMapper>();
            _restaurantAuthServiceMock = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(_restaurantRepoMock.Object, _mapperMock.Object, _restaurantAuthServiceMock.Object);
        }


        [Fact()]
        public async Task Handle_ForValidRequest_ShouldUpdateRestaurant()
        {
            // Arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand
            {
                Id = restaurantId,
                Name = "New Test",
                Description = "New Description",
                HasDelivery = true
            };

            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test"
            };

            _restaurantRepoMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(restaurant);

            _restaurantAuthServiceMock
                .Setup(x => x.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                .Returns(true);

            _mapperMock
                .Setup(mapper => mapper.Map<Restaurant>(command))
                .Returns(restaurant);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _restaurantRepoMock.Verify(r => r.UpdateRestauraunt(restaurant), Times.Once);
        }
    }
}