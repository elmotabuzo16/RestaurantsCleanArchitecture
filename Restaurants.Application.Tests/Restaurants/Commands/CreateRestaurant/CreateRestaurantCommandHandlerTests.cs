using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Restaurants.Domain.Repositories;
using AutoMapper;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using FluentAssertions;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            // Arrange
            var restaurantRepoMock = new Mock<IRestaurantsRepository>();
            var mapperMock = new Mock<IMapper>();
            var userContextMock = new Mock<IUserContext>();

            // ReturnAsync(1) - 1 because the return type of CreateRestaurantCommandHandler is int
            restaurantRepoMock
                .Setup(repo => repo.CreateRestaurant(It.IsAny<Restaurant>()))
                .ReturnsAsync(1);

            var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
            userContextMock
                .Setup(x => x.GetCurrentUser())
                .Returns(currentUser);

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mapperMock.Setup(x => x.Map<Restaurant>(command)).Returns(restaurant);


            var commandHandler = new CreateRestaurantCommandHandler(restaurantRepoMock.Object, mapperMock.Object, userContextMock.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(1); // Id
            restaurant.OwnerId.Should().Be("owner-id");
            restaurantRepoMock.Verify(r => r.CreateRestaurant(restaurant), Times.Once());
        }
    }
}