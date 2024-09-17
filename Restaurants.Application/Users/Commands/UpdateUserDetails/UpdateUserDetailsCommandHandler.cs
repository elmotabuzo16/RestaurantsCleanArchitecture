using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.UpdateUserDetailsComand
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private IUserContext _userContext;
        private IUserStore<User> _userStore;

        public UpdateUserDetailsCommandHandler(IUserContext userContext, IUserStore<User> userStore)
        {
            _userContext = userContext;
            _userStore = userStore;
        }

        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var dbUser = await _userStore.FindByIdAsync(user!._id, cancellationToken);

            if (dbUser == null)
            {
                throw new NotFoundException(nameof(User), user!._id);
            }

            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await _userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
