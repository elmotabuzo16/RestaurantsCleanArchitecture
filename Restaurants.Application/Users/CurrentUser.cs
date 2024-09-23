using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users
{
    public class CurrentUser
    {
        public CurrentUser(string Id, string email, IEnumerable<string> roles, string? nationality, DateOnly? dateOfBirth)
        {
            _id = Id;
            _email = email;
            _roles = roles;
            _dateOfBirth = dateOfBirth;
            _nationality = nationality;
        }

        public string _id { get; }
        public string _email { get; }
        public IEnumerable<string> _roles { get; }
        public DateOnly? _dateOfBirth { get; }
        public string? _nationality { get; }

        public bool IsInRole(string role)
        {
            if (_roles.Contains(role))
            {
                return true;
            }

            return false;
        }
    }
}
