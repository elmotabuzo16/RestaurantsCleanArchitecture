using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_WithAuthenticateUser_ShouldReturnCurrentUser()
        {
            // Arrange
            var dateOfBirth = new DateOnly(1990, 1, 1);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@test.com"),
                new Claim(ClaimTypes.Role, UserRoles.Admin),
                new Claim(ClaimTypes.Role, UserRoles.User),
                new Claim("Nationality", "Pinoy"),
                new Claim("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new  ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // Act
            var currentUser = userContext.GetCurrentUser();

            // Assert
            currentUser.Should().NotBeNull();
            currentUser._id.Should().Be("1");
            currentUser._email.Should().Be("test@test.com");
            currentUser._roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser._nationality.Should().Be("Pinoy");
            currentUser._dateOfBirth.Should().Be(dateOfBirth);
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidException()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // Act
            Action action = () => userContext.GetCurrentUser();

            // Assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is not present.");
        }
    }
}